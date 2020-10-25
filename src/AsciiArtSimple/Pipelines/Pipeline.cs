using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsciiArtSimple.Pipelines
{

    /// <summary>
    /// Define contract of Pipeline
    /// </summary>
    public interface IPipeline
    {
        /// <summary>
        /// Run pipeline
        /// </summary>
        /// <returns></returns>
        Task Execute();
    }

    public abstract class Pipeline  : IPipeline
    {
        protected IEnumerable<IPipelineStep> Steps { get; private set; }

        public Pipeline(IEnumerable<IPipelineStep> steps)
        {
            Steps = steps;
        }

        public abstract Task Execute();
    }
}