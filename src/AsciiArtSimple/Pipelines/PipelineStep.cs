using System.Threading.Tasks;
using AsciiArtSimple.Pipelines.MultiThreaded.Steps;

namespace AsciiArtSimple.Pipelines
{
    /// <summary>
    /// Define contract of PipelineStep
    /// </summary>
    public interface IPipelineStep
    {
        int Number { get; }

        PipelineStepStatus Status { get; }

        Task Create();
    }

    public abstract class PipelineStep : IPipelineStep
    {
        protected PipelineStep(int number)
        {
            Number = number;
            Status = PipelineStepStatus.Running;
        }

        public int Number { get; }

        public virtual PipelineStepStatus Status { get; protected set; }

        public abstract Task Create();
    }
}