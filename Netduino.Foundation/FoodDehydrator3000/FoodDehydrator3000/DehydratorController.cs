using System;
using System.Threading;
using Microsoft.SPOT;
using H = Microsoft.SPOT.Hardware;
using N = SecretLabs.NETMF.Hardware.Netduino;
using Netduino.Foundation.Sensors.Temperature;
using Netduino.Foundation.Relays;
using Netduino.Foundation.Sensors.Buttons;

namespace FoodDehydrator3000
{
    public class DehydratorController
    {
        // events
        public event EventHandler RunTimeElapsed = delegate { };

        // peripherals
        protected AnalogTemperature _tempSensor = null;
        protected H.PWM _heaterRelayPwm = null;
        //protected Relay _heaterRelay = null;
        protected Relay _fanRelay = null;

        // controllers
        PidController _pidController = null;

        // members
        public bool Running {
            get { return _running; }
        }
        protected bool _running = false;

        public TimeSpan RunningTimeLeft
        {
            get { return _runningTimeLeft; }
        }
        protected TimeSpan _runningTimeLeft = TimeSpan.MinValue;

        public DehydratorController(AnalogTemperature tempSensor, H.PWM heater, Relay fan)
        {
            _tempSensor = tempSensor;
            _heaterRelayPwm = heater;
            _fanRelay = fan;

            _pidController = new PidController(45000);
            _pidController.P = 0.001;
            _pidController.I = 0.00001;
            _pidController.D = 0;

        }


        public void TurnOff()
        {
            Debug.Print("Turning off.");
            this._fanRelay.IsOn = false;
            this._heaterRelayPwm.Stop();
        }

        public void TurnOn()
        {
            TurnOn(TimeSpan.MaxValue);
        }

        public void TurnOn(TimeSpan runningTime)
        {
            Debug.Print("Turning on.");
            this._runningTimeLeft = runningTime;
            this._running = true;

            this._fanRelay.IsOn = true;
            this._heaterRelayPwm.Frequency = 1.0 / 10.0; // 10 seconds
            this._heaterRelayPwm.DutyCycle = 0.5; // 50% on
            this._heaterRelayPwm.Start();
        }

        public void PowerButtonClicked()
        {
            if (this._running) {
                this.TurnOff();
            } else {
                this.TurnOn();
            }
        }

        
    }
}