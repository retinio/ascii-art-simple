using System.Text;

namespace AsciiArtSimple.Art.Figlet.Layouts
{
    /// <summary>
    /// Smushing mode
    /// </summary>
    public class LayoutSmushing : ILayout
    {

        public string[] Apply(Font font, string text)
        {
            var view = new string[font.Height];
            for (var currentLine = 0; currentLine < font.Height; currentLine++)
            {
                var lineBuilder = new StringBuilder();
                lineBuilder.Append(font[text[0]][currentLine]);
                var lastChar = text[0];

                for (var currentCharIndex = 1; currentCharIndex < text.Length; currentCharIndex++)
                {
                    var currentChar = text[currentCharIndex];
                    var currentCharacterLine = font[currentChar][currentLine];
                    if (lastChar != ' ' && currentChar != ' ')
                    {
                        if (lineBuilder[^1] == ' ')
                        {
                            lineBuilder[^1] = currentCharacterLine[0];
                        }
                        lineBuilder.Append(currentCharacterLine.Substring(1));
                    }
                    else
                    {
                        lineBuilder.Append(currentCharacterLine);
                    }
                    lastChar = currentChar;
                }

                view[currentLine] = lineBuilder.ToString();
            }

            return view;
        }
    }
}