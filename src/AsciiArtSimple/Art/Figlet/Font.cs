using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AsciiArtSimple.Art.Figlet.Layouts;

namespace AsciiArtSimple.Art.Figlet
{
    public class Font
    {
        private static readonly string Signature = "flf2a";
        private readonly IDictionary<byte, Character> _characters;

        /// <summary>
        /// Hard blank character.
        /// </summary>
        public char HardBlank { get; private set; }

        /// <summary>
        /// Font height.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Font baseline.
        /// </summary>
        public int BaseLine { get; private set; }

        /// <summary>
        ///  Maximum length.
        /// </summary>
        public int MaxLength { get; private set; }

        /// <summary>
        /// Old layout flags.
        /// </summary>
        public int OldLayout { get; private set; }

        private readonly List<string> _comments;

        /// <summary>
        /// Comment lines
        /// </summary>
        public IEnumerable<string> Comments => _comments.AsReadOnly();

        /// <summary>
        /// Comment lines count.
        /// </summary>
        public int CommentLinesCount { get; private set; }

        /// <summary>
        /// Layout rule
        /// </summary>
        public LayoutRule LayoutRule
        {
            get
            {
                return OldLayout switch
                {
                    -1 => LayoutRule.FullSize,
                    0 => LayoutRule.Fitting,
                    _ => LayoutRule.Smushing
                };
            }
        }

        /// <summary>
        /// Get the FIGlet character by ASCII code
        /// </summary>
        /// <param name="c">ASCII code of a char</param>
        /// <returns></returns>
        public Character this[char c]
        {
            get
            {
                var charCode = (byte)c;
                return _characters[charCode];
            }
        }

        private Font()
        {
            _characters = new Dictionary<byte, Character>();
            _comments = new List<string>();
        }

        /// <summary>
        /// Load FIGlet font from a file
        /// </summary>
        /// <param name="filePath">Path of file with a FIGlet font</param>
        /// <returns></returns>
        public static async Task<Font> LoadFromFileAsync(string filePath)
        {
            var font = new Font();
            using var reader = File.OpenText(filePath);
            await font.LoadFontSettingsAsync(reader);
            await font.LoadFontCommentsAsync(reader);
            await font.LoadCharacters(reader);
            return font;
        }

        private async Task LoadFontSettingsAsync(StreamReader stream)
        {
            var settings = await GetFontSettingsAsync(stream);
            if (settings.Length == 0)
                throw new NotSupportedException("The format file is unknown");

            HardBlank = settings[0][0];
            Height = ParseInt(settings[1]);
            BaseLine = ParseInt(settings[2]);
            MaxLength = ParseInt(settings[3]);
            OldLayout = ParseInt(settings[4]);
            CommentLinesCount = ParseInt(settings[5]);

            static int ParseInt(string value)
            {
                if (int.TryParse(value, out var result))
                    return result;
                throw new FormatException();
            }
        }

        private static async Task<string[]> GetFontSettingsAsync(TextReader reader)
        {
            var line = await reader.ReadLineAsync();
            if (line == null)
                throw new NotSupportedException("The format file is unknown");
            return line.Replace(Signature, "")
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private async Task LoadFontCommentsAsync(TextReader reader)
        {
            for (var i = 0; i < CommentLinesCount; i++)
            {
                var line = await reader.ReadLineAsync();
                _comments.Add(line);
            }
        }

        private async Task LoadCharacters(TextReader reader)
        {
            // All ASCII letters
            for (byte i = 32; i <= 126; i++)
            {
                var characterLines = new List<string>();

                var currentLine = await reader.ReadLineAsync() ?? string.Empty;
                var currentLineIndex = 0;
                while (currentLineIndex < Height)
                {
                    characterLines.Add(currentLine.TrimEnd('@').Replace(HardBlank, ' '));

                    if (currentLine.EndsWith(@"@@"))
                    {
                        break;
                    }

                    currentLine = await reader.ReadLineAsync() ?? string.Empty;
                    currentLineIndex++;
                }


                _characters.Add(i, new Character(characterLines));
            }
        }


    }
}