using Components.Aphid.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Aphid.Parser
{
    public class ControlFlowExpression : AphidExpression, IParentNode
    {
        public AphidTokenType Type { get; set; }

        public AphidExpression Condition { get; set; }

        public List<AphidExpression> Body { get; set; }

        public ControlFlowExpression(AphidTokenType type, AphidExpression condition, List<AphidExpression> body)
        {
            Type = type;
            Condition = condition;
            Body = body;
        }

        public virtual IEnumerable<AphidExpression> GetChildren()
        {
            return new[] { Condition }.Concat(Body);
        }
    }
}
