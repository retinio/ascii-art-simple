using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsciiArtSimple.Pipelines.MultiThreaded
{
    public class MultiThreadedPipeline : Pipeline
    {
        public MultiThreadedPipeline(IEnumerable<IPipelineStep> steps) : base(steps)
        {
        }

        public override async Task Execute()
        {
            await Task.Run(() =>
            {
                if (Steps == null || !Steps.Any()) return;
                Task.WaitAll(Steps.Select(step => Task.Run(step.Create)).ToArray());
            });
        }
    }
}