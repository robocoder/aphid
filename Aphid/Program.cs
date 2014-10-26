﻿using Components.Aphid;
using Components.Aphid.Interpreter;
using Components.Aphid.Lexer;
using Components.Aphid.Library;
using Components.Aphid.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aphid
{
    class Program
    {
        static void DisplayDirections()
        {
            Console.WriteLine("aphid [Script]");
            Environment.Exit(0);
        }

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                DisplayDirections();
            }
            else if (!File.Exists(args[0]))
            {
                Console.WriteLine("Could not find {0}", args[0]);
                Environment.Exit(1);
            }

            var code = File.ReadAllText(args[0]);

            EnvironmentLibrary.SetEnvArgs(true);

            var interpreter = new AphidInterpreter();

            try
            {
                interpreter.Interpret(code);
            }
            catch (AphidParserException exception)
            {
                var line = TokenHelper.GetIndexPosition(code, exception.UnexpectedToken.Index);
                
                Console.WriteLine(
                    "Unexpected {0} {1} on line {2}\r\n\r\n{3}\r\n", 
                    exception.UnexpectedToken.TokenType.ToString().ToLower(),
                    exception.UnexpectedToken.Lexeme,
                    line.Item1,
                    TokenHelper.GetCodeExcerpt(code, exception.UnexpectedToken));
            }
            catch (AphidRuntimeException exception)
            {
                Console.WriteLine("Unexpected runtime exception\r\n\r\n{0}\r\n", exception.Message);
            }
        }
    }
}
