using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsciiArtSimple.Art.Figlet;
using AsciiArtSimple.Art.Figlet.Layouts;

namespace AsciiArtSimple.Art
{
    /// <summary>
    /// AsciiLine 
    /// </summary>
    public class AsciiLine
    {
        private readonly Font _font;
        private readonly string _line;
        private readonly IDictionary<LayoutRule, ILayout> _layouts;

        /// <summary>
        /// Initialize instance
        /// </summary>
        /// <param name="font">FIGlet font</param>
        /// <param name="line">Input l ine</param>
        public AsciiLine(Font font, string line)
        {
            _font = font;
            _line = line;
            _layouts = new Dictionary<LayoutRule, ILayout>()
            {
                { LayoutRule.Fitting, new LayoutFitting() },
                { LayoutRule.FullSize, new LayoutFullSize() },
                { LayoutRule.Smushing, new LayoutSmushing() }
            };
        }

        public override string ToString()
        {
            var layout = _layouts[_font.LayoutRule];
            var result = layout.Apply(_font, _line);
            var width = result.Max(line => line?.Length ?? 0);
            var stringBuilder = new StringBuilder(_font.Height * (width + 1));
            
            foreach (var line in result)
            {
                stringBuilder.AppendLine(line);
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}