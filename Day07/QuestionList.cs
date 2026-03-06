using System;
using System.Collections.Generic;
using System.IO;
using ExamSystem.Models;

namespace ExamSystem.IO
{
    
    public class QuestionList
    {
        private readonly List<Question> questions;
        private readonly string logFileName;

        public int Count => questions.Count;

        public QuestionList(string logFileName)
        {
            if (string.IsNullOrWhiteSpace(logFileName))
                throw new ArgumentException("Log file name cannot be null or empty.", logFileName);

            this.logFileName = logFileName;
            questions = new List<Question>();
        }

        public void Add(Question question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));

            questions.Add(question);

            // Log to file
            LogToFile(question);
        }

        private void LogToFile(Question question)
        {
            try
            {
                using StreamWriter writer = new StreamWriter(logFileName, append: true);

                writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {question}");
                writer.WriteLine($"  Body    : {question.Body}");
                writer.WriteLine($"  Marks   : {question.Marks}");
                writer.WriteLine($"  Correct : {question.CorrectAnswer}");
                writer.WriteLine(new string('-', 60));
            }
            catch (IOException ex)
            {
                Console.WriteLine($"[Warning] Could not write to log file '{logFileName}': {ex.Message}");
            }
        }

        public Question this[int index]
        {
            get
            {
                if (index < 0 || index >= questions.Count)
                    throw new IndexOutOfRangeException($"Index {index} out of range.");

                return questions[index];
            }
        }

        public Question[] ToArray()
        {
            return questions.ToArray();
        }
    }
}