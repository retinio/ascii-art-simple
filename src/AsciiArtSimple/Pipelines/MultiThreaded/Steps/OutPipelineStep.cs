using System.Text;
using System.Threading.Tasks;
using AsciiArtSimple.Pipelines.MultiThreaded.Structs;

namespace AsciiArtSimple.Pipelines.MultiThreaded.Steps
{
    public abstract class OutPipelineStep : MultiThreadedPipelineStep
    {
        private readonly PriorityQueue _inputQueue;

        protected OutPipelineStep(int number, IPipelineStatus pipelineStatus, PriorityQueue inputQueue) : base(number, pipelineStatus)
        {
            _inputQueue = inputQueue;
        }

        public override Task Create()
        {
            return OutAsync();
        }

        private async Task OutAsync()
        {
            var builder = new StringBuilder();
            var isStatusPreviousStepComplited = false;
            var currentPosition = 1;
            while (true)
            {
                isStatusPreviousStepComplited = GetStatusPreviousStep(isStatusPreviousStepComplited);
                if (!isStatusPreviousStepComplited)
                {
                    var priority = _inputQueue.PeakPriority();
                    if (priority == -1 || currentPosition != priority) continue;
                }

                var line = _inputQueue.Dequeue();
                if (line == null) break;
                builder.AppendLine(line);
                currentPosition++;
            }

            await OutAsync(builder);
            Status = PipelineStepStatus.Complited;
        }

        protected abstract Task OutAsync(StringBuilder builder);
    }
}