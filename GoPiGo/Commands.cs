namespace GoPiGo
{
    /// <summary>
    /// Commands which can be send to the GoPiGo board
    /// </summary>
    public enum Commands
    {
        /// <summary>
        /// Command to receive the version
        /// </summary>
        Version = 20,
        /// <summary>
        /// Command to receive the batteryvoltage
        /// </summary>
        BatteryVoltage = 118,
        /// <summary>
        /// Command to receive the distance
        /// </summary>
        UltraSonic = 117,

        /// <summary>
        /// Command to execute a digital write to a certain pin
        /// </summary>
        DigitalWrite = 12,
        /// <summary>
        /// Command to execute a digital read to a certain pin
        /// </summary>
        DigitalRead = 13,
        /// <summary>
        /// Command to execute an analog read to a certain pin
        /// </summary>
        AnalogRead = 14,
        /// <summary>
        /// Command to execute an analog write to a certain pin
        /// </summary>
        AnalogWrite = 15,
        /// <summary>
        /// Command to receive the mode of a certain pin
        /// </summary>
        PinMode = 16,
        /// <summary>
        /// Command to move the robot forward
        /// </summary>
        MoveForward = 119,
        MoveForwardNoPid = 105,
        /// <summary>
        /// Command to  move the robot backwards
        /// </summary>
        MoveBackward = 115,
        MoveBackwardNoPid = 107,
        /// <summary>
        /// Turns left but keeps moving
        /// </summary>
        MoveLeft = 97,
        /// <summary>
        /// Rotates on the spot left
        /// </summary>
        RotateLeft = 98,
        /// <summary>
        /// Turns right but keeps moving
        /// </summary>
        MoveRight = 100,
        /// <summary>
        /// Rotates on the spot right
        /// </summary>
        RotateRight = 110,
        /// <summary>
        /// Stops the robot
        /// </summary>
        Stop = 120,
        /// <summary>
        /// Makes the robot move faster by 10 units
        /// </summary>
        IncreaseSpeedBy10 = 116,
        /// <summary>
        /// Makes the robot move slower by 10 units
        /// </summary>
        DecreaseSpeedBy10 = 103,
        /// <summary>
        /// Command to interact with motor one
        /// </summary>
        MotorOne = 111,
        /// <summary>
        /// Command to interact with motor two
        /// </summary>
        MotorTwo = 112,
        /// <summary>
        /// Sets the left motor speed
        /// </summary>
        SetLeftMotorSpeed = 70,
        /// <summary>
        /// Set the right motor speed
        /// </summary>
        SetRightMotorSpeed = 71,

        /// <summary>
        /// Rotates the servo to a certain degree
        /// </summary>
        RotateServo = 101,
        /// <summary>
        /// Enables servo interaction
        /// </summary>
        EnableServo = 61,
        /// <summary>
        /// Disables servo interaction
        /// </summary>
        DisableServo = 60,

        SetEncoderTargeting = 50,
        EnableEncoder = 51,
        DisableEncoder = 52,
        ReadEncoder = 53,

        EnableCommunicationTimeout = 80,
        DisableCommunicationTimeout = 81,
        ReadTimeoutStatus = 82,
        
        /// <summary>
        /// Tests the trim setting
        /// </summary>
        TrimTest = 30,
        /// <summary>
        /// Writes a new trim setting to the  gopigo
        /// </summary>
        WriteTrim = 31,
        /// <summary>
        /// Reads a new Trim setting to the gopigo
        /// </summary>
        ReadTrim = 32
    }
}
