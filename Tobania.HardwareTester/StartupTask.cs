
using GoPiGo;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace Tobania.HardwareTester
{
    public sealed class StartupTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            Task.Run(async () =>
            {
                var factory = DeviceFactory.Build;
                using (var gpg = factory.BuildGoPiGo())
                {
                    System.Diagnostics.Debug.WriteLine("Creating Leds...");
                    var led1 = factory.BuildLed(Pin.LedLeft);
                    var led2 = factory.BuildLed(Pin.LedRight);
                    System.Diagnostics.Debug.WriteLine("\tTesting left led");
                    led1.ChangeState(GoPiGo.Sensors.SensorStatus.On);
                    await Task.Delay(20);
                    System.Diagnostics.Debug.WriteLine("\tTesting right led");
                    led2.ChangeState(GoPiGo.Sensors.SensorStatus.On);
                    await Task.Delay(1500);
                    led1.ChangeState(GoPiGo.Sensors.SensorStatus.Off);
                    await Task.Delay(20);
                    led2.ChangeState(GoPiGo.Sensors.SensorStatus.Off);
                    System.Diagnostics.Debug.WriteLine("Done!\n");


                    //System.Diagnostics.Debug.WriteLine("Testing servo");
                    //gpg.MotorController().EnableServo();
                    //await Task.Delay(100);
                    //gpg.MotorController().RotateServo(0);
                    //await Task.Delay(1500);
                    //gpg.MotorController().RotateServo(180);
                    //await Task.Delay(1500);
                    //gpg.MotorController().DisableServo();
                    //System.Diagnostics.Debug.WriteLine("Done!\n");
                    //await Task.Delay(100);
                    //System.Diagnostics.Debug.WriteLine("Creating ultrasonic sensor...");
                    //var ultrasonic = factory.BuildUltraSonicSensor(Pin.Analog1);
                    //await Task.Delay(100);
                    //System.Diagnostics.Debug.WriteLine($"\tDistance measured: {ultrasonic.MeasureInCentimeters()} cm");
                    //System.Diagnostics.Debug.WriteLine("Done!\n");

                    //// wait for the thing to rest
                    //await Task.Delay(100);

                    System.Diagnostics.Debug.WriteLine("Testing motor controls...");

                    System.Diagnostics.Debug.WriteLine("\tForward for 5seconds");
                    gpg.MotorController().MoveForward();
                    await Task.Delay(5000);

                    System.Diagnostics.Debug.WriteLine("\tBackwards for 5seconds");
                    gpg.MotorController().MoveBackward();
                    await Task.Delay(5000);

                    System.Diagnostics.Debug.WriteLine("\tMoving left for 5seconds");
                    gpg.MotorController().MoveLeft();
                    await Task.Delay(5000);

                    System.Diagnostics.Debug.WriteLine("\tMoving Right for 5seconds");
                    gpg.MotorController().MoveRight();
                    await Task.Delay(5000);

                    System.Diagnostics.Debug.WriteLine("\tStopping for 1 second");
                    gpg.MotorController().Stop();
                    await Task.Delay(1000);

                    System.Diagnostics.Debug.WriteLine("\tRotating Left for 5seconds");
                    gpg.MotorController().RotateLeft();
                    await Task.Delay(5000);

                    System.Diagnostics.Debug.WriteLine("\tRotating Right for 5seconds");
                    gpg.MotorController().RotateRight();
                    await Task.Delay(5000);
                    gpg.MotorController().Stop();



                    System.Diagnostics.Debug.WriteLine("Done!\n");
                }
            }).Wait();
        }


    }
}
