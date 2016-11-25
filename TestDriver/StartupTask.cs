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
        private BackgroundTaskDeferral _deferral;
        private IGoPiGo _goPiGo;
        private ILed Left,Right;
        private IUltrasonicRangerSensor Sensor;
        private ThreadPoolTimer _timer;
        private int _count;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();

            _goPiGo = DeviceFactory.Build.BuildGoPiGo();
            Left = DeviceFactory.Build.BuildLed(Pin.LedLeft);
            Right = DeviceFactory.Build.BuildLed(Pin.LedRight);
            Sensor = DeviceFactory.Build.BuildUltraSonicSensor(Pin.Digital1);
            System.Diagnostics.Debug.WriteLine($"Firmware: {_goPiGo.GetFirmwareVersion()}");
            _timer = ThreadPoolTimer.CreatePeriodicTimer(TurnLeftGoForward, TimeSpan.FromSeconds(5));
        }

        private void TurnLeftGoForward(ThreadPoolTimer timer)
        {


            _goPiGo.MotorController().MoveForward();
            //_goPiGo.MotorController().EnableServo();
            //for (int i = 0; i < 180; i++)
            //{
            //    _goPiGo.MotorController().RotateServo(i);
            //    System.Diagnostics.Debug.WriteLine($"Turning Servo to {i} degrees");
            //    Task.Delay(5);
            //}
            //_goPiGo.MotorController().RotateServo(90);
            //_goPiGo.MotorController().DisableServo();
            if (_count == 0)
            {
                _goPiGo.MotorController().MoveForward();
                System.Diagnostics.Debug.WriteLine($"Moving Forward");
                Task.Delay(2000);
                _goPiGo.MotorController().Stop();
            }

            else if (_count == 1)
            {
                _goPiGo.MotorController().MoveBackward();
                System.Diagnostics.Debug.WriteLine($"Moving Backward");
                Task.Delay(2000);
                _goPiGo.MotorController().Stop();
            }

            else if (_count == 2)
            {
                _goPiGo.MotorController().RotateLeft();
                System.Diagnostics.Debug.WriteLine($"Rotating left");
                Task.Delay(2000);
                _goPiGo.MotorController().Stop();
            }

            else if (_count == 3)
            {
                _goPiGo.MotorController().RotateRight();
                System.Diagnostics.Debug.WriteLine($"Rotating Right");
                Task.Delay(2000);
                _goPiGo.MotorController().Stop();
            }
            else if (_count == 4)
            {
                _goPiGo.MotorController().MoveLeft();
                System.Diagnostics.Debug.WriteLine($"Moving left");
                Task.Delay(2000);
                _goPiGo.MotorController().Stop();
            }
            else if (_count == 5)
            {
                _goPiGo.MotorController().MoveRight();
                System.Diagnostics.Debug.WriteLine($"Moving right");
                Task.Delay(2000);
                _goPiGo.MotorController().Stop();
            }
            else if (_count == 6)
            {
                _goPiGo.MotorController().SetLeftMotorSpeed(10);
                System.Diagnostics.Debug.WriteLine($"Setting leftmotor speed to 10");
                Task.Delay(2000);
                _goPiGo.MotorController().Stop();
            }
            else if (_count == 7)
            {
                _goPiGo.MotorController().SetRightMotorSpeed(10);
                System.Diagnostics.Debug.WriteLine($"Setting rightmotor speed to 10");
                Task.Delay(2000);
                _goPiGo.MotorController().Stop();
            }
            else
            {
                _timer.Cancel();
                _deferral.Complete();
            }

            _count++;

        }
    }
}
