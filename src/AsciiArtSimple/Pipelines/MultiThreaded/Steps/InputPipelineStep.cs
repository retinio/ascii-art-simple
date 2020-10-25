using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
using AsciiArtSimple.Pipelines.MultiThreaded.Structs;

namespace AsciiArtSimple.Pipelines.MultiThreaded.Steps
{
    public class InputPipelineStep : MultiThreadedPipelineStep
    {
        private readonly string _inputFilePath;
        private readonly ConcurrentQueue<Line> _outputQueue;
        

        public InputPipelineStep(int number, IPipelineStatus pipelineStatus, string inputFilePath, ConcurrentQueue<Line> outputQueue) : base(number, pipelineStatus)
        {
            _inputFilePath = inputFilePath;
            _outputQueue = outputQueue;
        }

        public override Task Create()
        {
            return InputDataAsync();
        }

        private async Task InputDataAsync()
        {
            var linePosition = 1;
            using var reader = File.OpenText(_inputFilePath);
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                _outputQueue.Enqueue(new Line(linePosition, line));
                linePosition++;
            }

            Status = PipelineStepStatus.Complited;
        }
    }
}