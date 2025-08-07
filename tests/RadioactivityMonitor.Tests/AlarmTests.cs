using System.Reflection;
using Xunit;
using RadioactivityMonitor;
using RadioactivityMonitor.Tests.Fakes;

namespace RadioactivityMonitor.Tests{

    public class AlarmTests
    {
        private const double LOW_THRESHOLD = 17.0;
        private const double HIGH_THRESHOLD = 21.0;

        #region Default Test
        [Fact]
        public void Alarm_ShouldBeOff_WhenDefault()
        {
            var fakeSensor = new FakeSensor(LOW_THRESHOLD);
            var alarm = new Alarm(fakeSensor);
            
            alarm.Check();

            Assert.False(alarm.AlarmOn);
            Assert.Equal(0, alarm.AlarmCount);
        }
        #endregion

        #region Below/Above Threshold Tests
        [Theory]
        [InlineData(0.0)]
        [InlineData(LOW_THRESHOLD - 0.00000000001)]
        public void Alarm_ShouldBeOn_WhenValueBelowLowThreshold(double value)
        {
            var fakeSensor = new FakeSensor(value);
            var alarm = new Alarm(fakeSensor);

            alarm.Check();

            Assert.True(alarm.AlarmOn);
            Assert.Equal(1, alarm.AlarmCount);
        }

        [Theory]
        [InlineData(HIGH_THRESHOLD + 0.0000000001)]
        [InlineData(100.0)]
        public void Alarm_ShouldBeOn_WhenValueAboveHighThreshold(double value)
        {
            var fakeSensor = new FakeSensor(value);
            var alarm = new Alarm(fakeSensor);

            alarm.Check();

            Assert.True(alarm.AlarmOn);
            Assert.Equal(1, alarm.AlarmCount);
        }
        #endregion

        #region Safe Range Tests
        [Theory]
        [InlineData(LOW_THRESHOLD)]              // Exactly Low Threshold
        [InlineData(LOW_THRESHOLD + 0.00000000001)]    // Just above Low Threshold
        [InlineData(18.5)]                      // Within thresholds
        [InlineData(HIGH_THRESHOLD - 0.0000000001)]    // Just below High Threshold
        [InlineData(HIGH_THRESHOLD)]              // Exactly High Threshold
        public void Alarm_ShouldNotBeOn_WhenValueWithinThresholds(double value)
        {
            var fakeSensor = new FakeSensor(value);
            var alarm = new Alarm(fakeSensor);

            alarm.Check();

            Assert.False(alarm.AlarmOn);
            Assert.Equal(0, alarm.AlarmCount);
        }
        #endregion

        #region Alarm Count Tests
        [Fact]
        public void Alarm_ShouldIncrementCount_WhenTriggeredMultipleTimes()
        {
            const int TRIGGER_COUNT = 5;
            var fakeSensor = new FakeSensor(LOW_THRESHOLD - 1);
            var alarm = new Alarm(fakeSensor);

            // Trigger the alarm multiple times
            for (int i = 0; i < TRIGGER_COUNT; i++)
            {
                alarm.Check();
            }

            Assert.True(alarm.AlarmOn);
            Assert.Equal(TRIGGER_COUNT, alarm.AlarmCount);
        }

        [Fact]
        public void Alarm_ShouldNotIncrementCount_WhenInSafeRange(){
            var fakeSensor = new FakeSensor(LOW_THRESHOLD + 1);
            var alarm = new Alarm(fakeSensor);

            alarm.Check();
            alarm.Check();

            Assert.False(alarm.AlarmOn);
            Assert.Equal(0, alarm.AlarmCount);
        }
        #endregion

        #region Alarm Test After Triggered Once and Then Safe
        [Fact]
        public void Alarm_ShouldRemainOn_AfterTriggeredAndThenSafe(){
            var fakeSensor = new SequenceSensor([LOW_THRESHOLD - 1, LOW_THRESHOLD, LOW_THRESHOLD + 1]);
            var alarm = new Alarm(fakeSensor);

            alarm.Check();
            alarm.Check();
            alarm.Check();

            Assert.True(alarm.AlarmOn);
            Assert.Equal(1, alarm.AlarmCount);
        }

        [Fact]
        public void Alarm_ShouldIncrementCount_AfterTriggeredAndThenSafe(){
            var fakeSensor = new SequenceSensor([LOW_THRESHOLD - 1, LOW_THRESHOLD, LOW_THRESHOLD - 1, LOW_THRESHOLD + 1]);
            var alarm = new Alarm(fakeSensor);

            alarm.Check();
            alarm.Check();
            alarm.Check();
            alarm.Check();

            Assert.True(alarm.AlarmOn);
            Assert.Equal(2, alarm.AlarmCount);
        }
        #endregion

        #region Sensor Failure Tests
        [Fact]
        public void Alarm_ShouldThrowException_WhenSensorFails()
        {
            // Simulate a sensor failure if NextMeasure throws an exception in production.
            // Best if the Alarm class is designed to handle sensor failures.
            var failSensor = new FailSensor();
            var alarm = new Alarm(failSensor);

            Assert.Throws<Exception>(() => alarm.Check());
        }
        #endregion
    }
}