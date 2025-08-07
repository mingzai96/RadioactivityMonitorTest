using RadioactivityMonitor.Interfaces;

namespace RadioactivityMonitor.Tests.Fakes
{

    public class FailSensor : ISensor
    {
        public double NextMeasure()
        {
            throw new Exception("Sensor failure");
        }
    }
}