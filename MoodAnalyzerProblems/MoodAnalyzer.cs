﻿namespace MoodAnalyzerProblems
{
    public class MoodAnalyzer
    {
        public string message;
        public MoodAnalyzer()
        {

        }
        public MoodAnalyzer(string message)
        {
            this.message = message;
        }
        public string AnalyseMood()
        {
            try
            {
                if (message == null)
                {
                    throw new MoodAnalyzerException(MoodAnalyzerException.ExceptionType.NULL_MOOD, "message is Null");
                }
                if (message.Equals(""))
                {
                    throw new MoodAnalyzerException(MoodAnalyzerException.ExceptionType.EMPTY_MOOD, "message is Empty");

                }
                if (message.ToLower().Contains("sad"))
                {
                    return "Sad";
                }
                else
                {
                    return "Happy";
                }
            }
            catch (MoodAnalyzerException ex)
            {
                return ex.Message;
            }
        }

    }
}