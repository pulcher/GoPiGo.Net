namespace GoPiGo.Sensors
{
    /// <summary>
    /// Interface to access one of the led lights
    /// </summary>
    public interface ILed
    {
        /// <summary>
        /// Gets the current state of the led light
        /// </summary>
        SensorStatus CurrentState { get; }
        /// <summary>
        /// Changes the state of the led light
        /// </summary>
        /// <param name="newState">the new state</param>
        /// <returns>This ledlight (could be used like fluent api</returns>
        ILed ChangeState(SensorStatus newState);
    }

    internal class Led : Sensor<ILed>, ILed
    {
        internal Led(IGoPiGo device, Pin pin) : base(device, pin, PinMode.Output)
        {
        }
    }
}
