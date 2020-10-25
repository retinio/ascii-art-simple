using System.IO;
using System.Text;
using System.Threading.Tasks;
using AsciiArtSimple.Pipelines.MultiThreaded.Steps;

namespace AsciiArtSimple.Pipelines.Synchronous.Steps
{
    public class FileOutPipelineStep : PipelineStep
    {
        private readonly string _outputFilePath;
        private readonly StringBuilder _input;

        public FileOutPipelineStep(int number, StringBuilder input, string outputFilePath) : base(number)
        {
            _input = input;
            _outputFilePath = outputFilePath;
        }

        public override Task Create()
        {
            return FileOutAsync();
        }

        private async Task FileOutAsync()
        {
            await using StreamWriter sw = new StreamWriter(_outputFilePath, false, Encoding.ASCII, 65536);
            await sw.WriteLineAsync(_input);
            Status = PipelineStepStatus.Complited;
        }
    }
}