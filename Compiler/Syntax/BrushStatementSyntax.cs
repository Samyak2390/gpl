using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace gpl.Compiler.Syntax
{
    class BrushStatementSyntax : StatementSyntax
    {
        public override SyntaxKind Kind { get; }
        public Color Color { get; }
        private readonly Dictionary<string, Color> ColorMap;

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
