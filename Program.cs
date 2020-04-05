using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WordCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Stopwatch to determine the execution time for an application
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            if (args.Length != 1)
                throw new ArgumentException("Missing folder location");

            var popularWords = Directory.GetFiles(args[0]).AsParallel()
                .SelectMany(f => File.ReadLines(f)).AsParallel()
                .SelectMany(l => l.Split(' '))
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .Select(w => w)
                .GroupBy(w => w)
                .OrderByDescending(w => w.Count())
                .Take(5);

            // Stop measuring time
            stopwatch.Stop();

            foreach (var entry in popularWords)
            {
                Console.WriteLine($"{entry.Key}: {entry.Count()}");
            }

            // Format and display the TimeSpan value.
            var ts = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }
    }
}
