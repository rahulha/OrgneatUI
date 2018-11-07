using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Collector.Utilities
{
    public class Performance
    {
        private static PerformanceCounter cpuCounter;
        private static PerformanceCounter ramCounter;

        public Performance(Process P)
        {

            //cpuCounter = new PerformanceCounter();
            //cpuCounter.CategoryName = "Process";
            //cpuCounter.CounterName = "% Processor Time";
            //cpuCounter.InstanceName = this.LookForInstanceName(P);// "_Total";
            //Application.ProductName

            cpuCounter = new PerformanceCounter("Process", "% Processor Time", P.ProcessName);

            ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public String LookForInstanceName(Process P)
        {
            PerformanceCounterCategory processCategory = new PerformanceCounterCategory("Process");
            string[] instanceNames = processCategory.GetInstanceNames();
            string instanceName = "";

            foreach (string name in instanceNames)
                if (name.Contains(P.ProcessName))
                    using (PerformanceCounter processIdCounter = new PerformanceCounter("Process", "ID Process", name, true))
                    {
                        if (P.Id == (int)processIdCounter.RawValue)
                            instanceName = processIdCounter.InstanceName;
                    }

            return instanceName;
        }

        public static float GetProcessorUsage()
        {

            float cpu = (cpuCounter.NextValue() / Environment.ProcessorCount);

            //= cpuCounter.NextValue();

            return cpu;
        }

        public static float GetMemoryUsage()
        {
            float ram = ramCounter.NextValue();

            return ram;
        }

    }
}
