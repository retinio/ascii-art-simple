namespace AsciiArtSimple.Art.Figlet.Layouts
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILayout
    {
        /// <summary>
        /// Apply layout mode to a text
        /// </summary>
        /// <param name="font"></param>
        /// <param name="text">text</param>
        /// <returns></returns>
        string[] Apply(Font font, string text);
    }
}