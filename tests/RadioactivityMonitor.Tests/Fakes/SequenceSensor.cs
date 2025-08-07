using System.Collections.Generic;
using RadioactivityMonitor.Interfaces;

namespace RadioactivityMonitor.Tests.Fakes
{

    public class SequenceSensor : ISensor
    {
        private readonly Queue<double> _values;

        public SequenceSensor(IEnumerable<double> values)
        {
            _values = new Queue<double>(values);
        }

        public double NextMeasure()
        {
            return _values.Count > 1 ? _values.Dequeue() : _values.Peek();
        }
    }
}