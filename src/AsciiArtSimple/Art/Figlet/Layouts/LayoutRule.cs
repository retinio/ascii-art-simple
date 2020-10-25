namespace AsciiArtSimple.Art.Figlet.Layouts
{
    /// <summary>
    /// Layout modes
    /// http://www.jave.de/figlet/figfont.html#layoutmodes
    /// </summary>
    public enum LayoutRule
    {
        /// <summary>
        /// Separately called "Full Width" or "Full Height".
        /// </summary>
        FullSize,

        /// <summary>
        /// Separately called "Kerning or "Vertical Fitting".)
        /// </summary>
        Fitting,

        /// <summary>
        /// Same term for both axes
        /// </summary>
        Smushing
    }
}
