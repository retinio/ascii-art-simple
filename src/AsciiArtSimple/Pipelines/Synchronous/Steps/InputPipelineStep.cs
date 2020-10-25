using System.IO;
using System.Text;
using System.Threading.Tasks;
using AsciiArtSimple.Art;
using AsciiArtSimple.Art.Figlet;
using AsciiArtSimple.Pipelines.MultiThreaded.Steps;

namespace AsciiArtSimple.Pipelines.Synchronous.Steps
{
    public class InputPipelineStep : PipelineStep
    {
        private readonly StringBuilder _output;
        private readonly string _inputFilePath;
        private readonly string _fontFilePath;

        public InputPipelineStep(int number, string inputFilePath, string fontFilePath, StringBuilder output) : base(number)
        {
            _output = output;
            _inputFilePath = inputFilePath;
            _fontFilePath = fontFilePath;
        }

        public override Task Create()
        {
            return  InputDataAsync() ;
        }

        private async Task InputDataAsync()
        {
            var font = await Font.LoadFromFileAsync(_fontFilePath);
            using var reader = File.OpenText(_inputFilePath);
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                var ascii = new AsciiLine(font, line);
                _output.AppendLine(ascii.ToString());
            }

            Status = PipelineStepStatus.Complited;
            ;
        }
    }
}