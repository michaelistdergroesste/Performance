using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace Performance
{
    internal class ProcessTimer
    {
        PerformanceCounter pRam;
        PerformanceCounter pCpu;

        Queue<Measurement> qMeasurement;
        private int queueSize = 10;
        public int QueueSize
        {
            get { return queueSize; }
            set { queueSize = value; }
        }

        bool firstStart = false;

        public ProcessTimer()
        {
            //pCpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            InitialisierePerformanceCounter();
        }

        void InitialisierePerformanceCounter() // Initialisieren
        {
            pCpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            pRam = new PerformanceCounter("Memory", "% Committed Bytes In Use");
            PerformanceCounterCategory[] categories;
            categories = PerformanceCounterCategory.GetCategories();

            //    cpuCounter.CategoryName = "Processor";
            //    cpuCounter.CounterName = "% Processor Time";
            //    cpuCounter.InstanceName = "_Total"; // "_Total" entspricht der gesamten CPU Auslastung, Bei Computern mit mehr als 1 logischem Prozessor: "0" dem ersten Core, "1" dem zweiten...
        }

        
        
        /// <summary>
        /// Starten des Auslesens der Daten
        /// </summary>
        /// <param name="obj"></param>
        internal void DoWork(object obj)
        {
            qMeasurement = (Queue<Measurement>)obj;
            //qMeasurement = new Queue<Measurement>();
            while (true)
            {
                Thread.Sleep(1000); // alle 1 Sekunde.
                GetCPUusage();
                
            }
        }


        private void AddQueue(Measurement measurement)
        {
            qMeasurement.Enqueue(measurement);
            while (qMeasurement.Count > queueSize)
                qMeasurement.Dequeue();
        }

        void GetCPUusage() // Liefert die aktuelle Auslastung zurück
        {
            float retVal1 = pCpu.NextValue();
            Measurement cpu = new Measurement();
            cpu.Value = (int)(retVal1 * 100.0F);
            cpu.DateTime = DateTime.Now;
            cpu.DetectorId = 0;
            AddQueue(cpu);
            float retVal2 = pRam.NextValue();
            Measurement ram = new Measurement();
            ram.Value = (int)(retVal2 * 100.0F);
            ram.DateTime = DateTime.Now;
            ram.DetectorId = 0;
            AddQueue(ram);

        }

    }
}
