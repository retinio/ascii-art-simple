using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsciiArtSimple
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Clear();

            var sw = new Stopwatch();

            var factory = new PipelineBuilderFactory();
            var builder = factory.Create();
            var pipeline = builder.Build();

            sw.Start();

            await pipeline.Execute();

            sw.Stop();

            TimeSpan timeTaken = sw.Elapsed;
            string time = "Time taken: " + timeTaken.ToString(@"m\:ss\.fff");

            Console.Write('\n');
            Console.Write(time);
            Console.Write('\n');
        }
    }
}
