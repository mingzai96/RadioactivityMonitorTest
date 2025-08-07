using System;
using RadioactivityMonitor;

namespace RadioactivityMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var alarm = new Alarm();
            while (true)
            {
                alarm.Check();
                Console.WriteLine($"AlarmOn: {alarm.AlarmOn}, AlarmCount: {alarm.AlarmCount}");
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}