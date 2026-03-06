using System;

namespace ExamSystem.Models
{
    public abstract class Question
    {
        public string Header { get; protected set; }
        public string Body { get; protected set; }
        public int Marks { get; protected set; }
        public AnswerList Answers { get; protected set; }
        public Answer CorrectAnswer { get; protected set; }

        protected Question(string header, string body, int marks, AnswerList answers, Answer correctAnswer)
        {
            if (string.IsNullOrWhiteSpace(header))
                throw new ArgumentException("Header cannot be null or empty.", nameof(header));
            if (string.IsNullOrWhiteSpace(body))
                throw new ArgumentException("Body cannot be null or empty.", nameof(body));
            if (marks <= 0)
                throw new ArgumentException("Marks must be greater than zero.", nameof(marks));
            if (answers == null)
                throw new ArgumentNullException(nameof(answers));
            if (correctAnswer == null)
                throw new ArgumentNullException(nameof(correctAnswer));

            Header = header;
            Body = body;
            Marks = marks;
            Answers = answers;
            CorrectAnswer = correctAnswer;
        }

        public abstract void Display();
        public abstract bool CheckAnswer(Answer studentAnswer);

        protected void DisplayAnswers()
        {
            for (int i = 0; i < Answers.Count; i++)
                Console.WriteLine($"   {Answers[i]}");
        }

        public override string ToString() =>
            $"[{Header}] {Body} ({Marks} marks)";

        public override bool Equals(object? obj)
        {
            if (obj is Question other)
                return Header == other.Header && Body == other.Body;
            return false;
        }

        public override int GetHashCode() => HashCode.Combine(Header, Body);
    }
}
