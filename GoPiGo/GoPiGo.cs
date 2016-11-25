using System;
using System.Threading.Tasks;
using Windows.Devices.I2c;

namespace GoPiGo
{
    public interface IGoPiGo
    {
        string GetFirmwareVersion();
        byte DigitalRead(Pin pin);
        void DigitalWrite(Pin pin, byte value);
        int AnalogRead(Pin pin);
        void AnalogWrite(Pin pin, byte value);
        void PinMode(Pin pin, PinMode mode);
        decimal BatteryVoltage();
        IMotorController MotorController();
        //Currently not functioning
        //IEncoderController EncoderController();
        IGoPiGo RunCommand(Commands command, byte firstParam = Constants.Unused, byte secondParam = Constants.Unused, byte thirdParam = Constants.Unused);
    }

    public class GoPiGo : IGoPiGo
    {
        private readonly IMotorController _motorController;
        private readonly IEncoderController _encoderController;

        internal GoPiGo(I2cDevice device)
        {
            if (device == null) throw new ArgumentNullException(nameof(device));
            DirectAccess = device;
            _motorController = new MotorController(this);
            _encoderController = new EncoderController(this);
        }

        public IMotorController MotorController()
        {
            return _motorController;
        }

        public IEncoderController EncoderController()
        {
            return _encoderController;
        }

        internal I2cDevice DirectAccess { get; }
        internal static object Locker = new object();

        public string GetFirmwareVersion()
        {
            var buffer = new[] { (byte)Commands.Version, Constants.Unused, Constants.Unused, Constants.Unused };
            lock (Locker)
            {
                DirectAccess.Write(buffer);
                Task.Delay(5);
                DirectAccess.Read(buffer);
            }
            return $"{buffer[0]}";
        }

        public byte DigitalRead(Pin pin)
        {
            var buffer = new[] { (byte)Commands.DigitalRead, (byte)pin, Constants.Unused, Constants.Unused };
            var readBuffer = new byte[1];
            lock (Locker)
            {
                DirectAccess.Write(buffer);
                Task.Delay(100);//Give GoPiGo the time to Process
                DirectAccess.Read(readBuffer);
            }
            return readBuffer[0];
        }

        public void DigitalWrite(Pin pin, byte value)
        {
            var buffer = new[] { (byte)Commands.DigitalWrite, (byte)pin, value, Constants.Unused };
            lock (Locker)
            {
                DirectAccess.Write(buffer);
                Task.Delay(5); //Wait 5 sec for the command to complete
            }
        }

        public int AnalogRead(Pin pin)
        {
            var buffer = new[]
            {(byte) Commands.DigitalRead, (byte) Commands.AnalogRead, (byte) pin, Constants.Unused, Constants.Unused};
            lock (Locker)
            {
                DirectAccess.Write(buffer);
                Task.Delay(7); //Wait a few ms to process
                DirectAccess.Read(buffer);
            }
            return buffer[1] * 256 + buffer[2];
        }

        public void AnalogWrite(Pin pin, byte value)
        {
            var buffer = new[] { (byte)Commands.AnalogWrite, (byte)pin, value, Constants.Unused };
            lock (Locker)
            {
                DirectAccess.Write(buffer);
                Task.Delay(5); //Wait a few ms to process
            }
        }

        public void PinMode(Pin pin, PinMode mode)
        {
            var buffer = new[] { (byte)Commands.PinMode, (byte)pin, (byte)mode, Constants.Unused };
            lock (Locker)
            {
                DirectAccess.Write(buffer);
                Task.Delay(5);
            }
        }


        public decimal BatteryVoltage()
        {
            var buffer = new[] { (byte)Commands.BatteryVoltage, Constants.Unused, Constants.Unused, Constants.Unused };
            lock (Locker)
            {
                DirectAccess.Write(buffer);
                Task.Delay(1); //Wait a few ms to process
                DirectAccess.Read(buffer);
            }
            decimal voltage = buffer[1] * 256 + buffer[2];
            voltage = (5 * voltage / 1024) / (decimal).4;

            return Math.Round(voltage, 2);
        }

        public IGoPiGo RunCommand(Commands command, byte firstParam = Constants.Unused, byte secondParam = Constants.Unused, byte thirdParam = Constants.Unused)
        {
            var buffer = new[] { (byte)command, firstParam, secondParam, thirdParam };
            lock (Locker)
            {
                DirectAccess.Write(buffer);
                Task.Delay(5);
            }
            return this;
        }
    }
}
