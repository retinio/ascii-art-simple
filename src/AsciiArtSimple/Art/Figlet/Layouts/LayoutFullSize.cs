using System.Linq;
using System.Text;

namespace AsciiArtSimple.Art.Figlet.Layouts
{
    /// <summary>
    /// FullSize mode 
    /// </summary>
    public class LayoutFullSize : ILayout
    {
        
        public string[] Apply(Font font, string text)
        {
            var view = new string[font.Height];
            for (var currentLine = 0; currentLine < font.Height; currentLine++)
            {
                var lineBuilder = new StringBuilder();
                foreach (var character in text.Select(currentChar => font[currentChar]))
                {
                    lineBuilder.Append(character[currentLine]);
                    lineBuilder.Append(' ');
                }

                view[currentLine] = lineBuilder.ToString();
            }

            return view;
        }
    }
}