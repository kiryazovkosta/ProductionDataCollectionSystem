﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionEvaluator;

namespace MathExpressions.Application
{
    public class Calculator
    {
        public double Calculate(string expression, string expressionArgumentName, int argumentsCount, Dictionary<string, double> arguments)
        {
            if (argumentsCount != arguments.Count)
	        {
                string message = string.Format("Invalid arguments count. Expected {0} actual {1}", argumentsCount, arguments.Count);
		        throw new ArgumentException(message);
	        }

            var inputObject = ObjectBuilder.CreateObject(arguments);

            var reg = new TypeRegistry();
            reg.RegisterType(expressionArgumentName+"s", inputObject.GetType());
            reg.RegisterSymbol(expressionArgumentName, inputObject);
            reg.RegisterType("Math", typeof(Math));
            reg.RegisterType("Convert", typeof(Convert));
            var p = new CompiledExpression(expression) { TypeRegistry = reg };
            //p.Compile();
            return (double)p.Eval();
        }
    }
}
