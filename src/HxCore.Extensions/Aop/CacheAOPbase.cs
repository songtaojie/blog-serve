﻿using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Hx.Sdk.Common.Extensions;
using System.Linq;

namespace HxCore.Aops
{
    public abstract class CacheAOPbase : IInterceptor
    {
        public abstract void Intercept(IInvocation invocation);

        /// <summary>
        /// 自定义缓存的key
        /// </summary>
        /// <param name="prefix">键的前缀</param>
        /// <param name="invocation"></param>
        /// <returns></returns>
        protected string CustomCacheKey(IInvocation invocation, string prefix)
        {
            var typeName = invocation.TargetType.Name;
            var methodName = invocation.Method.Name;
            var keyList = new List<string>()
            {
                prefix,
                typeName.ToUpper(),
                methodName.ToUpper(),
            };
            foreach (var arg in invocation.Arguments)
            {
                var param = GetArgumentValue(arg);
                if(!string.IsNullOrEmpty(param)) keyList.Add(param);
            }
            return string.Join(":", keyList);
        }


        /// <summary>
        /// object 转 string
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        protected static string GetArgumentValue(object arg)
        {
            if (arg is DateTime || arg is DateTime?)
                return ((DateTime)arg).ToString("yyyyMMddHHmmss");

            if (arg is string or ValueType)
                return arg.ToString();

            if (arg != null)
            {
                if (arg is Expression)
                {
                    var obj = arg as Expression;
                    var result = Resolve(obj);
                    return result.MD5Encrypt();
                }
                else if (arg.GetType().IsClass)
                {
                    var result = Newtonsoft.Json.JsonConvert.SerializeObject(arg);
                    return result.MD5Encrypt();
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 解析lambda参数
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private static string Resolve(Expression expression)
        {
            if (expression is LambdaExpression)
            {
                LambdaExpression lambda = expression as LambdaExpression;
                expression = lambda.Body;
                return Resolve(expression);
            }
            if (expression is BinaryExpression)
            {
                BinaryExpression binary = expression as BinaryExpression;
                if (binary.Left is MemberExpression && binary.Right is ConstantExpression)//解析x=>x.Name=="123" x.Age==123这类
                    return ResolveFunc(binary.Left, binary.Right, binary.NodeType);
                if (binary.Left is MethodCallExpression && binary.Right is ConstantExpression)//解析x=>x.Name.Contains("xxx")==false这类的
                {
                    object value = (binary.Right as ConstantExpression).Value;
                    return ResolveLinqToObject(binary.Left, value, binary.NodeType);
                }
                if ((binary.Left is MemberExpression && binary.Right is MemberExpression)
                    || (binary.Left is MemberExpression && binary.Right is UnaryExpression))//解析x=>x.Date==DateTime.Now这种
                {
                    LambdaExpression lambda = Expression.Lambda(binary.Right);
                    Delegate fn = lambda.Compile();
                    ConstantExpression value = Expression.Constant(fn.DynamicInvoke(null), binary.Right.Type);
                    return ResolveFunc(binary.Left, value, binary.NodeType);
                }
            }
            if (expression is UnaryExpression)
            {
                UnaryExpression unary = expression as UnaryExpression;
                if (unary.Operand is MethodCallExpression)//解析!x=>x.Name.Contains("xxx")或!array.Contains(x.Name)这类
                    return ResolveLinqToObject(unary.Operand, false);
                if (unary.Operand is MemberExpression && unary.NodeType == ExpressionType.Not)//解析x=>!x.isDeletion这样的 
                {
                    ConstantExpression constant = Expression.Constant(false);
                    return ResolveFunc(unary.Operand, constant, ExpressionType.Equal);
                }
            }
            if (expression is MemberExpression && expression.NodeType == ExpressionType.MemberAccess)//解析x=>x.isDeletion这样的 
            {
                MemberExpression member = expression as MemberExpression;
                ConstantExpression constant = Expression.Constant(true);
                return ResolveFunc(member, constant, ExpressionType.Equal);
            }
            if (expression is MethodCallExpression)//x=>x.Name.Contains("xxx")或array.Contains(x.Name)这类
            {
                MethodCallExpression methodcall = expression as MethodCallExpression;
                return ResolveLinqToObject(methodcall, true);
            }
            var body = expression as BinaryExpression;
            //已经修改过代码body应该不会是null值了
            if (body == null)
                return string.Empty;
            var Operator = GetOperator(body.NodeType);
            var Left = Resolve(body.Left);
            var Right = Resolve(body.Right);
            string Result = string.Format("({0} {1} {2})", Left, Operator, Right);
            return Result;
        }

        private static string GetOperator(ExpressionType expressiontype)
        {
            switch (expressiontype)
            {
                case ExpressionType.And:
                    return "and";
                case ExpressionType.AndAlso:
                    return "and";
                case ExpressionType.Or:
                    return "or";
                case ExpressionType.OrElse:
                    return "or";
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                default:
                    throw new Exception(string.Format("不支持{0}此种运算符查找！" + expressiontype));
            }
        }

        private static string ResolveFunc(Expression left, Expression right, ExpressionType expressiontype)
        {
            var Name = (left as MemberExpression).Member.Name;
            var Value = (right as ConstantExpression).Value;
            var Operator = GetOperator(expressiontype);
            return Name + Operator + Value ?? "null";
        }

        private static string ResolveLinqToObject(Expression expression, object value, ExpressionType? expressiontype = null)
        {
            var MethodCall = expression as MethodCallExpression;
            var MethodName = MethodCall.Method.Name;
            switch (MethodName)
            {
                case "Contains":
                    if (MethodCall.Object != null)
                        return Like(MethodCall);
                    return In(MethodCall, value);
                case "Count":
                    return Len(MethodCall, value, expressiontype.Value);
                case "LongCount":
                    return Len(MethodCall, value, expressiontype.Value);
                default:
                    throw new Exception(string.Format("不支持{0}方法的查找！", MethodName));
            }
        }

        private static string In(MethodCallExpression expression, object isTrue)
        {
            var Argument1 = (expression.Arguments[0] as MemberExpression).Expression as ConstantExpression;
            var Argument2 = expression.Arguments[1] as MemberExpression;
            var Field_Array = Argument1.Value.GetType().GetFields().First();
            object[] Array = Field_Array.GetValue(Argument1.Value) as object[];
            List<string> SetInPara = new List<string>();
            for (int i = 0; i < Array.Length; i++)
            {
                string Name_para = "InParameter" + i;
                string Value = Array[i].ToString();
                SetInPara.Add(Value);
            }
            string Name = Argument2.Member.Name;
            string Operator = Convert.ToBoolean(isTrue) ? "in" : " not in";
            string CompName = string.Join(",", SetInPara);
            string Result = string.Format("{0} {1} ({2})", Name, Operator, CompName);
            return Result;
        }
        private static string Like(MethodCallExpression expression)
        {

            var Temp = expression.Arguments[0];
            LambdaExpression lambda = Expression.Lambda(Temp);
            Delegate fn = lambda.Compile();
            var tempValue = Expression.Constant(fn.DynamicInvoke(null), Temp.Type);
            string Value = string.Format("%{0}%", tempValue);
            string Name = (expression.Object as MemberExpression).Member.Name;
            string Result = string.Format("{0} like {1}", Name, Value);
            return Result;
        }


        private static string Len(MethodCallExpression expression, object value, ExpressionType expressiontype)
        {
            object Name = (expression.Arguments[0] as MemberExpression).Member.Name;
            string Operator = GetOperator(expressiontype);
            string Result = string.Format("len({0}){1}{2}", Name, Operator, value.ToString());
            return Result;
        }
    }
}
