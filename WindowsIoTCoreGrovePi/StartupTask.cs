using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using GrovePi;
using GrovePi.Sensors;
using System.Threading.Tasks;
using System.Diagnostics;


// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace WindowsIoTCoreGrovePi
{
    public sealed class StartupTask : IBackgroundTask
    {
        private BackgroundTaskDeferral deferral;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            deferral = taskInstance.GetDeferral();

            var led = DeviceFactory.Build.Led(Pin.DigitalPin6);
            var relay = DeviceFactory.Build.Relay(Pin.DigitalPin2);

            DeviceFactory.Build.RgbLcdDisplay().SetText("Hello World").SetBacklightRgb(0, 255, 255);

          Debug.WriteLine(  DeviceFactory.Build.GrovePi().GetFirmwareVersion());

    //        var light = DeviceFactory.Build.LightSensor(Pin.AnalogPin1);

            while (true)
            {
                for (byte i = 200; i < 255; i=(byte)(i+1))
                {
                    led.AnalogWrite(i);
                    Task.Delay(50).Wait();
                }
            }

           

           
            while (true)
            {

                relay.ChangeState(SensorStatus.On);
                //Debug.WriteLine(light.SensorValue());
                led.ChangeState(SensorStatus.On);
                Task.Delay(500).Wait();
                relay.ChangeState(SensorStatus.Off);
                led.ChangeState(SensorStatus.Off);
                Task.Delay(500).Wait();



            }

            // 
            // TODO: Insert code to start one or more asynchronous methods 
            //
        }
    }
}
