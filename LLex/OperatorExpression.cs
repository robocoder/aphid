﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LLex
{
    public class OperatorExpression : RegexExpression
    {
        public RegexTokenType Type { get; private set; }

        public OperatorExpression(RegexTokenType type)
        {
            Type = type;
        }
    }
}
