using System;
using System.Text;
using System.Threading.Tasks;
using AsciiArtSimple.Pipelines.MultiThreaded.Steps;

namespace AsciiArtSimple.Pipelines.Synchronous.Steps
{
    public class ConsoleOutPipelineStep : PipelineStep
    {
        private readonly StringBuilder _input;

        public ConsoleOutPipelineStep(int number, StringBuilder input) : base(number)
        {
            _input = input;
        }

        public override Task Create()
        {
            return ConsolOutAsync();
        }

        private async Task ConsolOutAsync()
        {
            await Console.Out.WriteLineAsync(_input);
            Status = PipelineStepStatus.Complited;
        }
    }
}