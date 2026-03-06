using System;

namespace ExamSystem.Models
{
    public class ChooseOneQuestion : Question
    {
        public ChooseOneQuestion(string header, string body, int marks, AnswerList answers, Answer correctAnswer)
            : base(header, body, marks, answers, correctAnswer)
        {
        }

        public override void Display()
        {
           
            Console.WriteLine($"\n  {Header}: {Body}  [{Marks} mark(s)] [Choose One]");
            DisplayAnswers();
        }

        public override bool CheckAnswer(Answer studentAnswer)
        {
            if (studentAnswer == null) return false;
            return studentAnswer.Equals(CorrectAnswer);
        }

        public override string ToString() => $"[Choose One] {base.ToString()}";
    }
}
