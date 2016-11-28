using System;
using System.Threading.Tasks;

namespace GoPiGo.Sensors
{
    public interface IUltrasonicRangerSensor
    {
        int MeasureInCentimeters();
    }

    internal class UltrasonicRangerSensor : IUltrasonicRangerSensor
    {
        private const byte CommandAddress = 117;
        private readonly GoPiGo _device;
        private readonly Pin _pin;

        internal UltrasonicRangerSensor(GoPiGo device, Pin pin)
        {
            _device = device;
            _pin = pin;
        }

        public int MeasureInCentimeters()
        {
            var buffer = new[] { CommandAddress, (byte)_pin, Constants.Unused, Constants.Unused };
            _device.WriteToI2C(buffer);
            Task.Delay(100);
            try
            {
                var b1 = _device.ReadByte();
                Task.Delay(5);
                var b2 = _device.ReadByte();
                return b1 * 256 + b2;
            }
            catch (Exception)
            {
                return -1;
            }

        }
    }
}
