using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading;
using Windows.ApplicationModel.Background;
using Windows.System.Threading;
using GoPiGo;
using GoPiGo.Sensors;
using System.Threading.Tasks;

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
                    led1.ChangeState(GoPiGo.Sensors.SensorStatus.On);
                    await Task.Delay(20);
                    led2.ChangeState(GoPiGo.Sensors.SensorStatus.On);
                    await Task.Delay(1500);
                    led1.ChangeState(GoPiGo.Sensors.SensorStatus.Off);
                    await Task.Delay(20); 
                    led2.ChangeState(GoPiGo.Sensors.SensorStatus.Off);

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
                        await Task.Delay(50);
                        var d = ultrasonic.MeasureInCentimeters();
                        while (d > 30)
                        {
                            await Task.Delay(500);
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
                            await Task.Delay(500);
                            d = ultrasonic.MeasureInCentimeters();
                            await Task.Delay(10);
                        }
                    }
                }
            }).Wait();
        }


    }
}
