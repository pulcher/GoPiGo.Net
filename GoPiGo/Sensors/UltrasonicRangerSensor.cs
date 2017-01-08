using System;
using System.Threading.Tasks;

namespace GoPiGo.Sensors
{
    /// <summary>
    /// Interface to access the Ultrasonic sensor
    /// </summary>
    public interface IUltrasonicRangerSensor
    {
        /// <summary>
        /// Gets the distance in CM to a next object.
        /// Beware! This sensor is only acurate for smaller distances. The more distance, the more issues!
        /// </summary>
        /// <returns>Distance in CM or -1 if something faulty occured</returns>
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
        /// <summary>
        /// Gets the distance in CM to a next object.
        /// Beware! This sensor is only acurate for smaller distances. The more distance, the more issues!
        /// </summary>
        /// <returns>Distance in CM or -1 if something faulty occured</returns>
        public int MeasureInCentimeters()
        {
            var buffer = new[] { CommandAddress, (byte)_pin, Constants.Unused, Constants.Unused };
            _device.WriteToI2C(buffer);
            Task.Delay(100).Wait();
            try
            {
                var b1 = _device.ReadByte();
                Task.Delay(5).Wait();
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
