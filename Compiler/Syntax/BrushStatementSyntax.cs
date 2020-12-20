using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace gpl.Compiler.Syntax
{
    /// <summary>
    /// Class representing the brush command.
    /// </summary>
    class BrushStatementSyntax : StatementSyntax
    {
        /// <summary>
        /// Kind of the syntax.
        /// </summary>
        public override SyntaxKind Kind { get; }

        /// <summary>
        /// Color of the brush.
        /// </summary>
        public Color Color { get; }
        private readonly Dictionary<string, Color> ColorMap;

        /// <summary>
        /// Constructor initializing available colors.
        /// </summary>
        /// <param name="kind">Kind of syntax.</param>
        /// <param name="color">Type of color</param>
        public BrushStatementSyntax(SyntaxKind kind, string color)
        {
            ColorMap = new Dictionary<string, Color>
            {
                {"red", Color.Red },
                {"blue", Color.Blue },
                {"green", Color.Green },
                {"yellow", Color.Yellow },
            };
            Kind = kind;
            Color = GetColor(color);
        }

        /// <summary>
        /// Method to find and return color.
        /// </summary>
        /// <param name="color">Name of the color in string.</param>
        /// <returns>Color struct</returns>
        private Color GetColor(string color)
        {
            if (ColorMap.ContainsKey(color))
            {
                return ColorMap[color];
            }
            return Color.Black;
        }
    }
}
