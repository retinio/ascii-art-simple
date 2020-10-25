namespace AsciiArtSimple.Pipelines.MultiThreaded.Structs
{
    public class Line
    {
        public int Position { get; }
        public string Data { get; }

        public Line(int position, string data)
        {
            Position = position;
            Data = data;
        }
    }
}