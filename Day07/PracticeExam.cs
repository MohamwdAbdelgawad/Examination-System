using System;
using System.Collections.Generic;

namespace ExamSystem.Models
{
    public class PracticeExam : Exam
    {
        public PracticeExam(int time, Subject subject, List<Question> questions)
            : base(time, subject, questions) { }

        public override void ShowExam()
        {
            Console.WriteLine("\n\n  ══════════════  PRACTICE EXAM RESULTS  ══════════════");

            foreach (var question in Questions)
            {
                Console.WriteLine($"\n  Q: {question.Body}");

                if (QuestionAnswerDictionary.TryGetValue(question, out var studentAns))
                    Console.WriteLine($"     Your answer : {studentAns}");
                else
                    Console.WriteLine("     Your answer : (no answer given)");

                Console.WriteLine($"     Correct     : {question.CorrectAnswer}");

                bool correct =
                    QuestionAnswerDictionary.TryGetValue(question, out var sa) &&
                    question.CheckAnswer(sa);

                Console.WriteLine($"     Result      : {(correct ? "Correct" : "Wrong")}");
            }

            Console.WriteLine();
            CorrectExam();

            Console.WriteLine("  ══════════════════════════════════════════════════════\n");
        }

        public override object Clone()
        {
            return new PracticeExam(Time, Subject, new List<Question>(Questions));
        }

        public override string ToString() => $"[Practice] {base.ToString()}";
    }
}