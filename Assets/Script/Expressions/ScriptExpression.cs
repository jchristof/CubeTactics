using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script {
    public class ScriptExpression {
        Dictionary<string, object> Variables = new Dictionary<string, object>();

        public void AddVariable(string name, object value) {
            Variables.Add(name, value);
        }

        public void RemoveVariable(string name) {
            Variables.Remove(name);
        }

        public void Assignment(string variable, string expression) {
            if (!Variables.Keys.Contains(variable))
                Variables.Add(variable, null);

            Expression e = new Expression(expression);
            e.EvaluateParameter += delegate(string name, ParameterArgs args) {
                ParameterResolver(name, args);
            };

            e.EvaluateFunction += delegate(string name, FunctionArgs args) {
                FunctionResolver(name, args);
            };
       

            Variables[variable] = e.Evaluate();
        }

        public bool Evaluate(string expression) {
            Expression e = new Expression(expression);
            e.EvaluateParameter += delegate(string name, ParameterArgs args) {
                ParameterResolver(name, args);
            };

            e.EvaluateFunction += delegate(string name, FunctionArgs args) {
                FunctionResolver(name, args);
            };

            bool value = Convert.ToBoolean(e.Evaluate());

            return value;
        }

        void FunctionResolver(string name, FunctionArgs args) {
            switch (name) {
                case "SecretOperation":
                    args.Result = (int)args.Parameters[0].Evaluate() + (int)args.Parameters[1].Evaluate();
                    break;
                default:
                    return;
            }
        }

        void ParameterResolver(string name, ParameterArgs args) {

            switch (name) {
                case "@Score":
                    args.Result = 0;
                    break;
                case "@PlayerX":
                    args.Result = 0;
                    break;
                case "@PlayerY":
                    args.Result = 0;
                    break;
                default:
                    args.Result = Variables[name];
                    break;
            }
            return;
        }
    }

    
}
