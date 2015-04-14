﻿using Components.Aphid.Lexer;
using Components.Aphid.Parser;
using Components.Aphid.Parser.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantispid
{
    public class TypeInferenceMutator : AphidMutator
    {
        private SymbolTable<ParserIdentifier> _idTable = new SymbolTable<ParserIdentifier>();

        private List<string> _returnIds = new List<string>();

        private ParserGeneratorConfig _config;

        public TypeInferenceMutator(ParserGeneratorConfig config)
        {
            _config = config;
        }

        protected override List<AphidExpression> MutateCore(AphidExpression expression, out bool hasChanged)
        {
            hasChanged = false;

            if (IsStatement && expression.Type == AphidNodeType.IdentifierExpression)
            {
                var idExp = expression.ToIdentifier();
                var currentId = ParserIdentifier.FromIdentifierExpression(idExp);
                ParserIdentifier inferredId;

                if (!_idTable.TryResolve(idExp.Identifier, out inferredId))
                {
                    _idTable.Add(idExp.Identifier, currentId);
                    hasChanged = true;
                }
                else
                {
                    if (!currentId.IsList && inferredId.IsList)
                    {
                        currentId.IsList = inferredId.IsList;
                        currentId.Type = inferredId.Type;
                        hasChanged = true;

                        return new List<AphidExpression> 
                        { 
                            new BinaryOperatorExpression(
                                currentId.ToIdentifierExpression(),
                                AphidTokenType.AssignmentOperator,
                                new IdentifierExpression(
                                    currentId.Type ?? _config.BaseClass, 
                                    new List<IdentifierExpression> { new IdentifierExpression("list") }))
                        };
                    }
                }
            }

            CallExpression funcExp;
            BinaryOperatorExpression binOpExp;

            if (expression.Type == AphidNodeType.CallExpression &&
                (funcExp = expression.ToCall()).FunctionExpression.Type == AphidNodeType.BinaryOperatorExpression &&
                ((binOpExp = funcExp.FunctionExpression.ToBinaryOperator()).Operator == AphidTokenType.MemberOperator) &&
                binOpExp.LeftOperand.Type == AphidNodeType.IdentifierExpression &&
                binOpExp.RightOperand.Type == AphidNodeType.IdentifierExpression &&
                binOpExp.RightOperand.ToIdentifier().Identifier == "Add")
            {
                var id = binOpExp.LeftOperand.ToIdentifier().Identifier;
                ParserIdentifier ids;

                if (!_idTable.TryResolve(id, out ids))
                {
                    _idTable.Add(id, ids = new ParserIdentifier() { Name = id });
                }

                if (!ids.IsList)
                {
                    ids.IsList = true;

                    if (ids.Type == null)
                    {
                        ids.Type = _config.BaseClass;
                    }

                    hasChanged = true;
                }
            }

            UnaryOperatorExpression unOpExp;

            if (expression.Type == AphidNodeType.UnaryOperatorExpression &&
                (unOpExp = expression.ToUnaryOperator()).Operator == AphidTokenType.retKeyword)
            {
                if (unOpExp.Operand.Type == AphidNodeType.IdentifierExpression)
                {
                    var retId = unOpExp.Operand.ToIdentifier();

                    if (!_returnIds.Contains(retId.Identifier))
                    {
                        hasChanged = true;
                        _returnIds.Add(retId.Identifier);
                    }
                }
            }
            //else if (expression.Type == AphidNodeType.BinaryOperatorExpression &&
            //    (binOpExp = expression.ToBinaryOperator()).

            return new List<AphidExpression> { expression };
            //throw new NotImplementedException();
        }
    }
}
