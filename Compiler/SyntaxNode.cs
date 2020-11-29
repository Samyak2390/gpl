using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

namespace gpl.Compiler
{
    public abstract class SyntaxNode
    {
        public abstract SyntaxKind Kind { get; }

        private string ParseTree;

        public IEnumerable<SyntaxNode> GetChildren()
        {
            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                if (typeof(SyntaxNode).IsAssignableFrom(property.PropertyType))
                {
                    var child = (SyntaxNode)property.GetValue(this);
                    if (child != null)
                        yield return child;
                }
                else if (typeof(IEnumerable<SyntaxNode>).IsAssignableFrom(property.PropertyType))
                {
                    var children = (IEnumerable<SyntaxNode>)property.GetValue(this);
                    foreach (var child in children)
                    {
                        if (child != null)
                            yield return child;
                    }
                }
            }
        }

        public string GetTree()
        {
            PrettyPrint(this);
            return ParseTree;
        }

        private void PrettyPrint(SyntaxNode node, string indent = "", bool isLast=true)
        {
            var marker = isLast ? "└──" : "├──";

            ParseTree += indent + marker + node.Kind;
            if(node is SyntaxToken t && t.Value != null)
            {
                ParseTree += " " + t.Value;
            }
            ParseTree += Environment.NewLine;
            indent += isLast ? "   " : "│  ";

            var lastChild = node.GetChildren().LastOrDefault();

            foreach (var child in node.GetChildren())
                PrettyPrint(child, indent, child == lastChild);
        }
    }
}
