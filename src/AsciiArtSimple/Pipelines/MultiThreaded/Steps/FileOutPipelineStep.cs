using System.IO;
using System.Text;
using System.Threading.Tasks;
using AsciiArtSimple.Pipelines.MultiThreaded.Structs;

namespace AsciiArtSimple.Pipelines.MultiThreaded.Steps
{
    public class FileOutPipelineStep : OutPipelineStep
    {
        private readonly string _outputFilePath;

        public FileOutPipelineStep(int number, IPipelineStatus pipelineStatus, PriorityQueue inputQueue, string outputFilePath) : base(number, pipelineStatus, inputQueue)
        {
            _outputFilePath = outputFilePath;
        }

        protected override async Task OutAsync(StringBuilder builder)
        {
            await using var sw = new StreamWriter(_outputFilePath, false, Encoding.ASCII, 65536);
            await sw.WriteLineAsync(builder);
        }
    }
}