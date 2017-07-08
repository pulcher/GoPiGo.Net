using GoPiGo;
using GoPiGo.Sensors;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace TestDriver
{
    public sealed class StartupTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            Task.Run(async () =>
            {
                var factory = DeviceFactory.Build;
                using (var gopigo = factory.BuildGoPiGo())
                {
                    var led1 = factory.BuildLed(Pin.LedLeft);
                    var led2 = factory.BuildLed(Pin.LedRight);
                    led1.ChangeState(SensorStatus.On);
                    await Task.Delay(20);
                    led2.ChangeState(SensorStatus.On);
                    await Task.Delay(1500);
                    led1.ChangeState(SensorStatus.Off);
                    await Task.Delay(20); 
                    led2.ChangeState(SensorStatus.Off);

                    DateTime dtStart = DateTime.Now;
                    var ultrasonic = factory.BuildUltraSonicSensor(Pin.Analog1);
                    gopigo.MotorController().EnableServo();
                    gopigo.MotorController().RotateServo(90);
                    gopigo.MotorController().DisableServo();
                    while ((DateTime.Now - dtStart).TotalMinutes < 5)
                    {
                        System.Diagnostics.Debug.WriteLine("battery: " + gopigo.BatteryVoltage());
                        gopigo.MotorController().Stop();
                        gopigo.MotorController().MoveForward();
                        var d = 31;
                        while (d > 30)
                        {
                            await Task.Delay(50);
                            d = ultrasonic.MeasureInCentimeters();
                            System.Diagnostics.Debug.WriteLine($"Driving forward with {d} cm of space");


                        }
                        gopigo.MotorController().Stop();

                        while (d < 30)
                        {
                            led1.ChangeState(GoPiGo.Sensors.SensorStatus.On);
                            gopigo.MotorController().RotateLeft();
                            await Task.Delay(250);
                            gopigo.MotorController().Stop();
                            led1.ChangeState(GoPiGo.Sensors.SensorStatus.Off);
                            await Task.Delay(50);
                            d = ultrasonic.MeasureInCentimeters();
                            await Task.Delay(10);
                        }
                    }
                }
            }).Wait();
        }


    }
}
