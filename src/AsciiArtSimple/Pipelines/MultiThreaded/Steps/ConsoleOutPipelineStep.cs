using System;
using System.Text;
using System.Threading.Tasks;
using AsciiArtSimple.Pipelines.MultiThreaded.Structs;

namespace AsciiArtSimple.Pipelines.MultiThreaded.Steps
{
    public class ConsoleOutPipelineStep : OutPipelineStep
    {
        public ConsoleOutPipelineStep(int number, IPipelineStatus pipelineStatus, PriorityQueue inputQueue) : base(number, pipelineStatus, inputQueue)
        {
        }

        protected override async Task OutAsync(StringBuilder builder)
        {
            await Console.Out.WriteLineAsync(builder);
        }
    }
}