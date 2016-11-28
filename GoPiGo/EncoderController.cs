using System;

namespace GoPiGo
{
    public interface IEncoderController
    {
        IEncoderController SetEncoderTargetingOn(State motorOneState, State motorTwoStatem, int target);
        int ReadEncoder(Motor motor);
        IEncoderController EnableEncoders();
        IEncoderController DisableEncoders();
    }

    public class EncoderController : IEncoderController
    {
        private readonly GoPiGo _goPiGo;

        public EncoderController(GoPiGo goPiGo)
        {
            _goPiGo = goPiGo;
        }

        public IEncoderController SetEncoderTargetingOn(State motorOneState, State motorTwoState, int target)
        {
            var motorSelect = (int)motorOneState * 2 + (int)motorTwoState;
            _goPiGo.RunCommand(Commands.SetEncoderTargeting, (byte)motorSelect, (byte)(target / 256), (byte)(target % 256));
            return this;
        }

        public int ReadEncoder(Motor motor)
        {
            var buffer = new[] { (byte)Commands.ReadEncoder, (byte)motor, Constants.Unused, Constants.Unused };
            _goPiGo.WriteToI2C(buffer);

            try
            {
                var b1 = _goPiGo.ReadByte();
                var b2 = _goPiGo.ReadByte();
                return b1 * 256 + b2;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public IEncoderController EnableEncoders()
        {
            _goPiGo.RunCommand(Commands.EnableEncoder);
            return this;
        }

        public IEncoderController DisableEncoders()
        {
            _goPiGo.RunCommand(Commands.DisableEncoder);
            return this;
        }
    }
}
