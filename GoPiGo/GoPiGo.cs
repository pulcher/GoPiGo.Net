using System;
using System.Threading.Tasks;
using Windows.Devices.I2c;

namespace GoPiGo
{
    /// <summary>
    /// Interface to access the GoPiGo
    /// </summary>
    public interface IGoPiGo : IDisposable
    {
        /// <summary>
        /// Gets the Firmwareversion of the gopigo
        /// </summary>
        /// <returns></returns>
        string GetFirmwareVersion();
        /// <summary>
        /// Digital reads a certain pin
        /// </summary>
        /// <param name="pin">The pin to read from </param>
        /// <returns>the set of bytes</returns>
        byte DigitalRead(Pin pin);
        /// <summary>
        /// Digital writes to a certain pin
        /// </summary>
        /// <param name="pin">The pin to write to</param>
        /// <param name="value">The value to write to</param>
        void DigitalWrite(Pin pin, byte value);
        /// <summary>
        /// Analog reads a certain pin
        /// </summary>
        /// <param name="pin">The pin to read</param>
        /// <returns>The value read from the pin</returns>
        int AnalogRead(Pin pin);
        /// <summary>
        /// Analog writes to a certain pin
        /// </summary>
        /// <param name="pin">The pin to write</param>
        /// <param name="value">The value to write</param>
        void AnalogWrite(Pin pin, byte value);
        void PinMode(Pin pin, PinMode mode);
        /// <summary>
        /// Gets the battery voltage
        /// </summary>
        /// <returns>The battery voltage</returns>
        decimal BatteryVoltage();
        /// <summary>
        /// Gets the motor controller
        /// </summary>
        /// <returns>The motor controller</returns>
        IMotorController MotorController();
        /// <summary>
        /// THIS IS NOT FULLY FUNCTIONING!
        /// Gets the encoder motor controller. 
        /// </summary>
        /// <returns>The motor encoder controller</returns>
        IEncoderController EncoderController();
        /// <summary>
        /// Runs a manual command
        /// </summary>
        /// <param name="command">The command to execute</param>
        /// <param name="firstParam">The first parameter</param>
        /// <param name="secondParam">The Second parameter</param>
        /// <param name="thirdParam">The Third Parameter</param>
        /// <returns>the gopigo</returns>
        IGoPiGo RunCommand(Commands command, byte firstParam = Constants.Unused, byte secondParam = Constants.Unused, byte thirdParam = Constants.Unused);
    }

    public class GoPiGo : IGoPiGo
    {
        const int GoPiGoAddress = 0x08;
        private readonly IMotorController _motorController;
        private readonly IEncoderController _encoderController;
        internal I2cDevice Device { get; }
        internal GoPiGo(I2cDevice device)
        {
            Device = device;
            _motorController = new MotorController(this);
            _encoderController = new EncoderController(this);
        }

        internal bool WriteToI2C(byte[] block)
        {
            try
            {
                Device.Write(block);
                Task.Delay(5).Wait();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        internal byte ReadByte()
        {
            byte[] bytes = new byte[1];
            Device.Read(bytes);
            return bytes[0];
        }
        public IMotorController MotorController()
        {
            return _motorController;
        }

        public IEncoderController EncoderController()
        {
            return _encoderController;
        }


        public string GetFirmwareVersion()
        {
            var buffer = new[] { (byte)Commands.Version, Constants.Unused, Constants.Unused, Constants.Unused };
            WriteToI2C(buffer);
            var firmware = ReadByte();
            return $"{firmware}";
        }

        public byte DigitalRead(Pin pin)
        {
            var buffer = new[] { (byte)Commands.DigitalRead, (byte)pin, Constants.Unused, Constants.Unused };
            WriteToI2C(buffer);
            Task.Delay(100).Wait();
            var data = ReadByte();
            return data;
        }

        public void DigitalWrite(Pin pin, byte value)
        {
            var buffer = new[] { (byte)Commands.DigitalWrite, (byte)pin, value, Constants.Unused };
            WriteToI2C(buffer);

        }

        public int AnalogRead(Pin pin)
        {
            var buffer = new[]
            {(byte) Commands.AnalogRead, (byte) pin, Constants.Unused, Constants.Unused};
            WriteToI2C(buffer);
            Task.Delay(7).Wait();
            try
            {
                var b1 = ReadByte();
                Task.Delay(5).Wait();
                var b2 = ReadByte();
                return b1 * 256 + b2;
            }
            catch (Exception)
            {
                return -1;
            }

        }

        public void AnalogWrite(Pin pin, byte value)
        {
            var buffer = new[] { (byte)Commands.AnalogWrite, (byte)pin, value, Constants.Unused };
            WriteToI2C(buffer);
        }

        public void PinMode(Pin pin, PinMode mode)
        {
            var buffer = new[] { (byte)Commands.PinMode, (byte)pin, (byte)mode, Constants.Unused };
            WriteToI2C(buffer);
        }


        public decimal BatteryVoltage()
        {
            var buffer = new[] { (byte)Commands.BatteryVoltage, Constants.Unused, Constants.Unused, Constants.Unused };
            WriteToI2C(buffer);
             //Wait a few ms to process
            Task.Delay(1).Wait();
            try
            {
                var b1 = ReadByte();
                Task.Delay(5).Wait();
                var b2 = ReadByte();
                decimal v = b1 * 256 + b2;
                v = (5 * v / 1024) / (decimal)0.4;
                return Math.Round(v, 2);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public IGoPiGo RunCommand(Commands command, byte firstParam = Constants.Unused, byte secondParam = Constants.Unused, byte thirdParam = Constants.Unused)
        {
            var buffer = new[] { (byte)command, firstParam, secondParam, thirdParam };
            WriteToI2C(buffer);
            Task.Delay(5).Wait();
            return this;
        }

        public void Dispose()
        {
            if (this.Device != null)
            {
                this.MotorController().Stop();
                this.Device.Dispose();
            }
        }
    }
}
