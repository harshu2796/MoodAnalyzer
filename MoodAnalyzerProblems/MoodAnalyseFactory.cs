﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MoodAnalyzerProblems
{
    public class MoodAnalyzerFactory
    {
        public static object CreateMoodAnalyzer(string className, string ConstructorName)
        {
            string pattern = @"." + ConstructorName + "$";
            Match result = Regex.Match(className, pattern);
            if (result.Success)
            {
                try
                {
                    Assembly executing = Assembly.GetExecutingAssembly();
                    Type moodAnalyseType = executing.GetType(className);
                    return Activator.CreateInstance(moodAnalyseType);
                }
                catch (ArgumentNullException)
                {
                    throw new MoodAnalyzerException(MoodAnalyzerException.ExceptionType.NO_SUCH_CLASS, "Class Not Found");
                }
            }
            else
            {
                throw new MoodAnalyzerException(MoodAnalyzerException.ExceptionType.NO_SUCH_METHOD, "Constructor Not Found");
            }
        }
        public static object CreateMoodAnalyzerUsingParameterizedConstrctor(string className, string ConstructorName, string message)
        {
            Type type = typeof(MoodAnalyzer);
            if (type.Name.Equals(className) || type.FullName.Equals(className))
            {
                if (type.Name.Equals(ConstructorName))
                {
                    ConstructorInfo ctor = type.GetConstructor(new[] { typeof(string) });
                    object instance = ctor.Invoke(new object[] { message });
                    return instance;
                }
                else
                {
                    throw new MoodAnalyzerException(MoodAnalyzerException.ExceptionType.NO_SUCH_CLASS, "Class Not Found");
                }
            }
            else
            {
                throw new MoodAnalyzerException(MoodAnalyzerException.ExceptionType.NO_SUCH_METHOD, "Constructor Not Found");

            }
        }
        public static string InvokeAnalyzerMood(string message, string methodName)
        {
            try
            {
                Type type = Type.GetType("MoodAnalyzerProblems.MoodAnalyzer");
                object moodAnalyzerObject = MoodAnalyzerFactory.CreateMoodAnalyzerUsingParameterizedConstrctor("MoodAnalyzerProblems.MoodAnalyzer", "MoodAnalyzer", message);
                MethodInfo analyzerMoodInfo = type.GetMethod(methodName);
                object mood = analyzerMoodInfo.Invoke(moodAnalyzerObject, null);
                return mood.ToString();
            }
            catch (NullReferenceException)
            {
                throw new MoodAnalyzerException(MoodAnalyzerException.ExceptionType.NO_SUCH_METHOD, "Method is not found");
            }
        }
        public static string SetField(string message, string fieldName)
        {
            try
            {
                MoodAnalyzer moodAnalyzer = new MoodAnalyzer();
                Type type = typeof(MoodAnalyzer);
                FieldInfo field = type.GetField(fieldName, BindingFlags.Public | BindingFlags.Instance);
                if (message == null)
                {
                    throw new MoodAnalyzerException(MoodAnalyzerException.ExceptionType.NO_SUCH_FIELD, "Message Should not be null");
                }
                field.SetValue(moodAnalyzer,message);
                return moodAnalyzer.message;
            }
            catch (NullReferenceException)
            {
                throw new MoodAnalyzerException(MoodAnalyzerException.ExceptionType.NO_SUCH_FIELD, "Field is  not Found");
            }
        }
    }
}
