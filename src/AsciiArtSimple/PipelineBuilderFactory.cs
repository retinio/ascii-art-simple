using System;
using AsciiArtSimple.Pipelines;
using AsciiArtSimple.Pipelines.MultiThreaded;
using AsciiArtSimple.Pipelines.Synchronous;

namespace AsciiArtSimple
{
    public class PipelineBuilderFactory
    {
        public IPipelineBuilder Create()
        {
            var type = Environment.GetEnvironmentVariable("RUN_TYPE");
            if (!string.IsNullOrEmpty(type) && type.Equals("multi")) return CreateMultiThreadedPipelineBuilder();
            return CreateSynchronousPipeline();
        }

        private static IPipelineBuilder CreateSynchronousPipeline()
        {
            var fontPath = GetFontPath();
            var inputPath = GetInputPath();
            var output = GetOutputPath();
            return new SynchronousPipelineBuilder(fontPath, inputPath, output);
        }


        private static IPipelineBuilder CreateMultiThreadedPipelineBuilder()
        {
            var fontPath = GetFontPath();
            var inputPath = GetInputPath();
            var output = GetOutputPath();
            var convertorCount = GetConvertor();
            return new MultiThreadedPipelineBuilder(fontPath, inputPath, convertorCount: convertorCount, outputFilePath: output);
        }

        private static string GetFontPath()
        {
            var font = Environment.GetEnvironmentVariable("FONT_FILE_PATH");
            return string.IsNullOrEmpty(font) ? "./fonts/contessa.flf" : font;
        }

        private static string GetInputPath()
        {
            var input = Environment.GetEnvironmentVariable("INPUT_FILE_PATH");
            if (string.IsNullOrEmpty(input)) throw new ArgumentNullException("INPUT_FILE_PATH", "No input file specified.");
            return input;
        }

        private static string GetOutputPath()
        {
            return Environment.GetEnvironmentVariable("OUTPUT_FILE_PATH");
        }

        private static int GetConvertor()
        {
            var convertors = Environment.GetEnvironmentVariable("CONVERTORS_COUNT");
            if (string.IsNullOrEmpty(convertors)) return 1;
            return !int.TryParse(convertors, out var c) ? 1 : c;
        }
    }
}