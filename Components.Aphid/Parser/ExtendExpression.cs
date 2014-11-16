using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components.Aphid.Parser
{
    public class ExtendExpression : AphidExpression, IParentNode
    {
        public string Type { get; set; }

        public ObjectExpression Object { get; set; }

        public ExtendExpression(string type, ObjectExpression obj)
        {
            Type = type;
            Object = obj;
        }

        public IEnumerable<AphidExpression> GetChildren()
        {
            return new[] { Object };
        }
    }
}
