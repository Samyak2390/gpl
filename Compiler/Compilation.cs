using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler
{
    public class Compilation
    {
        public Compilation(SyntaxTree syntaxTree)
           : this(null, syntaxTree)
        {
            SyntaxTree = syntaxTree;
        }

        private Compilation(Compilation previous, SyntaxTree syntaxTree)
        {
            Previous = previous;
            SyntaxTree = syntaxTree;
        }

        public Compilation Previous { get; }
        public SyntaxTree SyntaxTree { get; }

        public EvaluationResult Evaluate(Dictionary<VariableSymbol, object> variables)
        {
       
            var evaluator = new Evaluator(GlobalScope.Statement, variables);
            var value = evaluator.Evaluate();
            return new EvaluationResult(ImmutableArray<Diagnostic>.Empty, value);
        }
    }
}
