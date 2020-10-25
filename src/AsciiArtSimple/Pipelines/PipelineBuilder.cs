using System.Collections.Generic;

namespace AsciiArtSimple.Pipelines
{
    public interface IPipelineBuilder
    {
        IPipeline Build();
    }

    public abstract class PipelineBuilder : IPipelineBuilder
    {
        protected IList<IPipelineStep> Steps { get; private set; }

        protected PipelineBuilder()
        {
            Steps = new List<IPipelineStep>();
        }

        protected void AddStep(IPipelineStep step)
        {
            Steps.Add(step);
        }

        public abstract IPipeline Build();
    }
}