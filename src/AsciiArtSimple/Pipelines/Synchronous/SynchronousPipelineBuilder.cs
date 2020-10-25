using System.Collections.Generic;
using System.Text;
using AsciiArtSimple.Pipelines.Synchronous.Steps;

namespace AsciiArtSimple.Pipelines.Synchronous
{
    public class SynchronousPipelineBuilder : PipelineBuilder
    {
        private readonly StringBuilder _output;
        private readonly string _inputFilePath;
        private readonly string _ouputFilePath;
        private readonly string _fontPath;

        public SynchronousPipelineBuilder(string fontPath, string inputInputFilePath, string outputFilePath = null)
        {
            _output = new StringBuilder();
            _inputFilePath = inputInputFilePath;
            _ouputFilePath = outputFilePath;
            _fontPath = fontPath;
        }

        public override IPipeline Build()
        {
            Steps.Add(new InputPipelineStep(1, _inputFilePath, _fontPath, _output));
            Steps.Add(
                _ouputFilePath == null
                    ? (IPipelineStep)new ConsoleOutPipelineStep(2, _output)
                    : new FileOutPipelineStep(2, _output, _ouputFilePath)
                );
            
            return new SynchronousPipeline(Steps);
        }
    }
}