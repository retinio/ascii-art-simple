using System.Text;

namespace AsciiArtSimple.Art.Figlet.Layouts
{
    /// <summary>
    /// Fitting mode
    /// </summary>
    public class LayoutFitting : ILayout
    {
        public string[] Apply(Font font, string text)
        {
            var view = new string[font.Height];
            for (var currentLine = 0; currentLine < font.Height; currentLine++)
            {
                var lineBuilder = new StringBuilder();
                foreach (var currentChar in text)
                {
                    var currentCharacterLine = font[currentChar][currentLine];
                    lineBuilder.Append(currentCharacterLine);
                }

                view[currentLine] = lineBuilder.ToString();
            }

            return view;
        }
    }
}