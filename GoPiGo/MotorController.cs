using System;

namespace GoPiGo
{
    /// <summary>
    /// Interface to access the motor controls
    /// </summary>
    public interface IMotorController
    {
        /// <summary>
        /// Moves the robot forward
        /// </summary>
        /// <returns>This</returns>
        IMotorController MoveForward();
        /// <summary>
        /// Moves the robot forward without pid control (Proportional integral derivate controller)
        /// </summary>
        /// <returns>This</returns>
        IMotorController MoveForwardNoPid();
        /// <summary>
        /// Moves the robot backwards
        /// </summary>
        /// <returns>This</returns>
        IMotorController MoveBackward();
        /// <summary>
        /// Moves the robot backwards without pid control (Proportional integral derivate controller)
        /// </summary>
        /// <returns>This</returns>
        IMotorController MoveBackwardNoPid();
        /// <summary>
        /// Turns the robot left, but keeps its momentum
        /// </summary>
        /// <returns>This</returns>
        IMotorController MoveLeft();
        /// <summary>
        /// Rotates the robot left on the spot
        /// </summary>
        /// <returns>This</returns>
        IMotorController RotateLeft();
        /// <summary>
        /// Turns the robot right but keeps it momentum
        /// </summary>
        /// <returns>This</returns>
        IMotorController MoveRight();
        /// <summary>
        /// Rotates the robot left on the spot
        /// </summary>
        /// <returns>This</returns>
        IMotorController RotateRight();
        /// <summary>
        /// Stops the robot
        /// </summary>
        /// <returns>This</returns>
        IMotorController Stop();
        /// <summary>
        /// Increments the speed with 10 units
        /// </summary>
        /// <returns>This</returns>
        IMotorController IncreaseSpeedBy10();
        /// <summary>
        /// Decrements the speed with 10 units
        /// </summary>
        /// <returns>This</returns>
        IMotorController DecreaseSpeedBy10();
       /// <summary>
       /// Manual controls motor one
       /// </summary>
       /// <param name="direction">The direction</param>
       /// <param name="speed">The speed</param>
       /// <returns>This</returns>
        IMotorController ControlMotorOne(int direction, int speed);
        /// <summary>
        /// Rotates the servo to a certain degree
        /// </summary>
        /// <param name="degrees">Degrees to turn</param>
        /// <returns>This</returns>
        IMotorController RotateServo(int degrees);
        /// <summary>
        /// Enables the servo to rotate
        /// </summary>
        /// <returns>This</returns>
        IMotorController EnableServo();
        /// <summary>
        /// Disables the servo to rotate
        /// </summary>
        /// <returns>This</returns>
        IMotorController DisableServo();
        /// <summary>
        /// Sets the left motor speed 
        /// </summary>
        /// <param name="speed">The speed to set</param>
        /// <returns></returns>
        IMotorController SetLeftMotorSpeed(int speed);
        /// <summary>
        /// Sets the right motor speed
        /// </summary>
        /// <param name="speed">The speed to set</param>
        /// <returns>This</returns>
        IMotorController SetRightMotorSpeed(int speed);
    }

    public class MotorController : IMotorController
    {
        internal MotorController(IGoPiGo goPiGo)
        {
            if (goPiGo == null) throw new ArgumentNullException(nameof(goPiGo));
            GoPiGo = goPiGo;
        }

        public IGoPiGo GoPiGo { get;private set; }


        public IMotorController MoveForward()
        {
            GoPiGo.RunCommand(Commands.MoveForward);
            return this;
        }

        public IMotorController MoveForwardNoPid()
        {
            GoPiGo.RunCommand(Commands.MoveForwardNoPid);
            return this;
        }

        public IMotorController MoveBackward()
        {
            GoPiGo.RunCommand(Commands.MoveBackward);
            return this;
        }

        public IMotorController MoveBackwardNoPid()
        {
            GoPiGo.RunCommand(Commands.MoveBackwardNoPid);
            return this;
        }

        public IMotorController MoveLeft()
        {
            GoPiGo.RunCommand(Commands.MoveLeft);
            return this;
        }

        public IMotorController RotateLeft()
        {
            GoPiGo.RunCommand(Commands.RotateLeft);
            return this;
        }

        public IMotorController MoveRight()
        {
            GoPiGo.RunCommand(Commands.MoveRight);
            return this;
        }

        public IMotorController RotateRight()
        {
            GoPiGo.RunCommand(Commands.RotateRight);
            return this;
        }

        public IMotorController Stop()
        {
            GoPiGo.RunCommand(Commands.Stop);
            return this;
        }

        public IMotorController IncreaseSpeedBy10()
        {
            GoPiGo.RunCommand(Commands.IncreaseSpeedBy10);
            return this;
        }

        public IMotorController DecreaseSpeedBy10()
        {
            GoPiGo.RunCommand(Commands.DecreaseSpeedBy10);
            return this;
        }

        public IMotorController ControlMotorOne(int direction, int speed)
        {
            GoPiGo.RunCommand(Commands.MotorOne, (byte)direction, (byte)speed);
            return this;
        }

        public IMotorController ControlMotorTwo(int direction, int speed)
        {
            GoPiGo.RunCommand(Commands.MotorTwo, (byte)direction, (byte)speed);
            return this;
        }

        public IMotorController RotateServo(int degrees)
        {
            GoPiGo.RunCommand(Commands.RotateServo, (byte)degrees);
            return this;
        }

        public IMotorController EnableServo()
        {
            GoPiGo.RunCommand(Commands.EnableServo);
            return this;
        }

        public IMotorController DisableServo()
        {
            GoPiGo.RunCommand(Commands.DisableServo);
            return this;
        }

        public IMotorController SetLeftMotorSpeed(int speed)
        {
            speed = Math.Min(speed, 255);
            GoPiGo.RunCommand(Commands.SetLeftMotorSpeed, (byte)speed);
            return this;
        }

        public IMotorController SetRightMotorSpeed(int speed)
        {
            speed = Math.Min(speed, 255);
            GoPiGo.RunCommand(Commands.SetRightMotorSpeed, (byte)speed);
            return this;
        }

        public IMotorController SetSpeed(int speed)
        {
            SetLeftMotorSpeed(speed);
            SetRightMotorSpeed(speed);
            return this;
        }
    }
}
