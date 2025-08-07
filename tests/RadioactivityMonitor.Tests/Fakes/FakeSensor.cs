using RadioactivityMonitor.Interfaces;

namespace RadioactivityMonitor.Tests.Fakes
{

    public class FakeSensor : ISensor
    {
        private readonly double _value;

        public FakeSensor(double value)
        {
            _value = value;
        }

        public double NextMeasure()
        {
            return _value;
        }
    }
}