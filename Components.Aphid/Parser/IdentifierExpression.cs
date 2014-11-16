using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components.Aphid.Parser
{
    public class IdentifierExpression : AphidExpression
    {
        public override AphidNodeType Type
        {
            get { return AphidNodeType.IdentifierExpression; }
        }

        public string Identifier { get; set; }

        public IdentifierExpression(string identifier)
        {
            Identifier = identifier;
        }

        public override string ToString()
        {
            return Identifier;
        }
    }
}
