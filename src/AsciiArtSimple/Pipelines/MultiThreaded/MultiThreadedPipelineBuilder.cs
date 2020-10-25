using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using AsciiArtSimple.Pipelines.MultiThreaded.Steps;
using AsciiArtSimple.Pipelines.MultiThreaded.Structs;

namespace AsciiArtSimple.Pipelines.MultiThreaded
{
    public class MultiThreadedPipelineBuilder : PipelineBuilder, IPipelineStatus, IDisposable
    {
        private readonly string _fontPath;
        private readonly string _inputFilePath;
        private readonly string _ouputFilePath;
        private readonly int _convertorCount;
        private readonly ConcurrentQueue<Line> _linesQueue;
        private readonly PriorityQueue _priorityQueue;

        public MultiThreadedPipelineBuilder(string fontPath, string inputInputFilePath, int convertorCount = 1,  string outputFilePath = null)
        {
            _fontPath = fontPath;
            _inputFilePath = inputInputFilePath;
            _ouputFilePath = outputFilePath;
            _linesQueue = new ConcurrentQueue<Line>();
            _priorityQueue = new PriorityQueue(true);
            _convertorCount = convertorCount;
        }

        public override IPipeline Build()
        {
            Steps.Add( new InputPipelineStep(1, this,_inputFilePath, _linesQueue));

            for (var i = 0; i < _convertorCount; i++)
                Steps.Add(new ConvertorPipelineStep(2, this, _fontPath, _linesQueue, _priorityQueue));

            Steps.Add(
                _ouputFilePath == null
                    ? (IPipelineStep)new ConsoleOutPipelineStep(3, this, _priorityQueue)
                    : new FileOutPipelineStep(3, this, _priorityQueue, _ouputFilePath)
            );

            return new MultiThreadedPipeline(Steps);
        }

        public PipelineStepStatus GetStatusPreviousStep(IPipelineStep step)
        {
            var steps = Steps.Where(s => s.Number == step.Number - 1);
            var stepsCount = steps.Count();
            if (stepsCount == 0) return PipelineStepStatus.Complited;

            var complitedSteps = Steps.Count(s => s.Number == step.Number - 1 && s.Status == PipelineStepStatus.Complited);
            return complitedSteps == stepsCount ? PipelineStepStatus.Complited : PipelineStepStatus.Running;
        }

        public void Dispose()
        {
            foreach (var step in Steps)
            {
                ((IDisposable)step).Dispose();
            }
        }
    }
}