using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsciiArtSimple.Pipelines.Synchronous
{
    public class SynchronousPipeline : Pipeline
    {
        public SynchronousPipeline(IEnumerable<IPipelineStep> steps) : base(steps)
        {
        }

        public override async Task Execute()
        {
            if (Steps == null || !Steps.Any()) return;
            
            foreach (var step in Steps)
            {
                await Task.WhenAll(step.Create());
            }
        }
    }
}