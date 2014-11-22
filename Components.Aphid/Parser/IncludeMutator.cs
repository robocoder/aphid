using Components.Aphid.Interpreter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Components.Aphid.Parser
{
    public class IncludeMutator : AphidMutator
    {
        AphidLoader _loader = new AphidLoader(null);

        public AphidLoader Loader
        {
            get { return _loader; }
        }

        protected override List<AphidExpression> MutateCore(AphidExpression expression, out bool hasChanged)
        {
            var loadExp = expression as LoadScriptExpression;

            if (loadExp == null)
            {
                hasChanged = false;

                return null;
            }

            hasChanged = true;

            var scriptExp = loadExp.FileExpression as StringExpression;

            if (scriptExp == null)
            {
                throw new AphidRuntimeException(
                    "Invalid load script operand '{0}'.",
                    loadExp.FileExpression);
            }

            var script = _loader.FindScriptFile(StringParser.Parse(scriptExp.Value));

            if (!File.Exists(script))
            {
                throw new AphidRuntimeException("Could not find script '{0}'.", scriptExp.Value);
            }

            return AphidParser.Parse(File.ReadAllText(script));
        }
    }
}
