using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WordCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
                throw new ArgumentException("Missing folder location");

            var popularWords = Directory.GetFiles(args[0]).AsEnumerable()//.AsParallel()
                .SelectMany(f => File.ReadLines(f)).AsEnumerable()//.AsParallel()
                .SelectMany(l => l.Split(' ')).AsEnumerable()//.AsParallel()
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .Select(w => w)
                .GroupBy(w => w)
                .OrderByDescending(w => w.Count())
                .Take(5);


            foreach(var entry in popularWords)
            {
                Console.WriteLine($"{entry.Key}: {entry.Count()}");
            }
        }
    }
}
