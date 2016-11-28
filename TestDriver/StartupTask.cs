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
                    var ultra = factory.BuildUltraSonicSensor(Pin.Analog1);
                    var led = factory.BuildLed(Pin.LedLeft);
                    gopigo.MotorController().MoveForward();
                    await Task.Delay(1000);
                    led.ChangeState(SensorStatus.On);
                    gopigo.MotorController().RotateLeft();
                    await Task.Delay(1000);
                    led.ChangeState(SensorStatus.Off);
                    gopigo.MotorController().RotateRight();
                    await Task.Delay(1000);
                    led.ChangeState(SensorStatus.On);
                    gopigo.MotorController().MoveBackward();
                    await Task.Delay(1000);
                    led.ChangeState(SensorStatus.Off);
                    gopigo.MotorController().Stop();
                    gopigo.MotorController().EnableServo();
                    for (int i = 0; i < 180; i+=10)
                    {
                        gopigo.MotorController().RotateServo(i);
                        await Task.Delay(1000);
                    }
                    gopigo.MotorController().RotateServo(90);
                    gopigo.MotorController().DisableServo();
                    System.Diagnostics.Debug.WriteLine($"Distance: {ultra.MeasureInCentimeters()}cm");
                    await Task.Delay(1000);
                }
            }).Wait();
        }


    }
}
