using System;

namespace ExamSystem.Models
{
    public class TrueFalseQuestion : Question
    {
        public TrueFalseQuestion(string header, string body, int marks, Answer correctAnswer)
            : base(header, body, marks, BuildAnswers(), correctAnswer)
        {
            if (correctAnswer.Id != 1 && correctAnswer.Id != 2)
                throw new ArgumentException("TrueFalseQuestion correct answer must be True (1) or False (2).");
        }

        private static AnswerList BuildAnswers()
        {
            var list = new AnswerList();
            list.Add(new Answer(1, "True"));
            list.Add(new Answer(2, "False"));
            return list;
        }

        public override void Display()
        {
            Console.WriteLine($"\n  {Header}: {Body}  [{Marks} mark(s)] [True/False]");
            DisplayAnswers();
        }

        public override bool CheckAnswer(Answer studentAnswer)
        {
            if (studentAnswer == null) return false;
            return studentAnswer.Equals(CorrectAnswer);
        }

        public override string ToString() => $"[True/False] {base.ToString()}";
    }
}
