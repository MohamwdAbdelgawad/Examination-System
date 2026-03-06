using System;
using System.Collections.Generic;

namespace ExamSystem.Models
{
    public class ChooseAllQuestion : Question
    {
        // Stores all correct answers for multi-select
        public List<Answer> CorrectAnswers { get; }

        public ChooseAllQuestion(string header, string body, int marks, AnswerList answers, List<Answer> correctAnswers)
            : base(header, body, marks, answers, correctAnswers[0])
        {
            if (correctAnswers == null || correctAnswers.Count == 0)
                throw new ArgumentException("ChooseAllQuestion must have at least one correct answer.");

            CorrectAnswers = correctAnswers;
        }

        public override void Display()
        {
            Console.WriteLine($"\n  {Header}: {Body}  [{Marks} mark(s)] [Choose All That Apply]");
            DisplayAnswers();
        }

        public bool CheckAnswers(List<Answer> studentAnswers)
        {
            if (studentAnswers == null || studentAnswers.Count != CorrectAnswers.Count)
                return false;

            // Create copies to avoid modifying original lists
            var studentSorted = new List<Answer>(studentAnswers);
            var correctSorted = new List<Answer>(CorrectAnswers);

            studentSorted.Sort();
            correctSorted.Sort();

            for (int i = 0; i < correctSorted.Count; i++)
                if (!studentSorted[i].Equals(correctSorted[i]))
                    return false;

            return true;
        }

        public override bool CheckAnswer(Answer studentAnswer)
        {
            if (studentAnswer == null) return false;

            foreach (var ca in CorrectAnswers)
                if (ca.Equals(studentAnswer))
                    return true;

            return false;
        }

        public override string ToString() => $"[Choose All] {base.ToString()}";
    }
}