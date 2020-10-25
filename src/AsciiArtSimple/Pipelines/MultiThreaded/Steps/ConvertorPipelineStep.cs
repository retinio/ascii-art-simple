using System.Collections.Concurrent;
using System.Threading.Tasks;
using AsciiArtSimple.Art;
using AsciiArtSimple.Art.Figlet;
using AsciiArtSimple.Pipelines.MultiThreaded.Structs;

namespace AsciiArtSimple.Pipelines.MultiThreaded.Steps
{
    public class ConvertorPipelineStep : MultiThreadedPipelineStep
    {
        private readonly string _fontFilePath;
        private readonly ConcurrentQueue<Line> _inputQueue;
        private readonly PriorityQueue _outputQueue;
        
        public ConvertorPipelineStep(int number, IPipelineStatus pipelineStatus, string fontFilePath, ConcurrentQueue<Line> inputQueue, PriorityQueue outputQueue) : base(number, pipelineStatus)
        {
            _fontFilePath = fontFilePath;
            _inputQueue = inputQueue;
            _outputQueue = outputQueue;
        }

        public override Task Create()
        {
            return ConvertDataAsync();
        }

        private async Task ConvertDataAsync()
        {
            var font = await Font.LoadFromFileAsync(_fontFilePath);
            var isStatusPreviousStepComplited = false;
            while (true)
            {
                isStatusPreviousStepComplited = GetStatusPreviousStep(isStatusPreviousStepComplited);
                if (!_inputQueue.TryDequeue(out var result) && isStatusPreviousStepComplited) break;
                if (result == null) continue;
                var ascii = new AsciiLine(font, result.Data);
                _outputQueue.Enqueue(result.Position, ascii.ToString());
            }

            Status = PipelineStepStatus.Complited;
        }
    }
}