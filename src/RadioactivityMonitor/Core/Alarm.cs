using RadioactivityMonitor.Interfaces;

namespace RadioactivityMonitor
{
    public class Alarm
    {
        private const double LowThreshold = 17;
        private const double HighThreshold = 21;

        // Change to ISensor to allow for dependency injection
        private ISensor _sensor;

        private bool _alarmOn = false;
        private long _alarmCount = 0;

        public Alarm()
        {
            // for default constructor, use a real sensor
            _sensor = new Sensor();
        }

        public Alarm(ISensor sensor)
        {
            // for constructor with sensor, use the provided sensor so that we can inject a fake sensor for testing
            // Also added a null check just in case this constructor is called with a null sensor in production code
            _sensor = sensor ?? new Sensor();
        }

        public void Check()
        {
            double value = _sensor.NextMeasure();

            // Fix the | operator
            if (value < LowThreshold || HighThreshold  < value)
            {
                _alarmOn = true;
                _alarmCount += 1;
            }
        }

        public bool AlarmOn
        {
            get { return _alarmOn; }
        }

        public long AlarmCount
        {
            get { return _alarmCount; }
        }
    }
}
