using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace gpl.Compiler.Syntax
{
    /// <summary>
    /// Class representing the pen command.
    /// </summary>
    class PenStatementSyntax : StatementSyntax
    {
        public override SyntaxKind Kind { get; }
        public Color Color { get; }
        private readonly Dictionary<string, Color> ColorMap;

        /// <summary>
        /// Constructor initializing pen command's parameters.
        /// </summary>
        /// <param name="kind">Syntax Kind</param>
        /// <param name="color">Name of the color in string.</param>
        public PenStatementSyntax(SyntaxKind kind, string color)
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
        /// Method the provides the available color.
        /// </summary>
        /// <param name="color">Name of the color as a string.</param>
        /// <returns></returns>
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
