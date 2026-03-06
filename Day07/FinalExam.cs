using System;
using System.Collections.Generic;

namespace ExamSystem.Models
{
    public class FinalExam : Exam
    {
        public FinalExam(int time, Subject subject, List<Question> questions)
            : base(time, subject, questions) { }

        public override void ShowExam()
        {
            Console.WriteLine("\n\n  ══════════════  FINAL EXAM SUMMARY  ══════════════");

            foreach (var question in Questions)
            {
                Console.WriteLine($"\n  Q: {question.Body}");

                if (QuestionAnswerDictionary.TryGetValue(question, out var studentAns))
                    Console.WriteLine($"     Your answer : {studentAns}");
                else
                    Console.WriteLine("     Your answer : (no answer given)");

            }

            Console.WriteLine("\n  Your answers have been recorded. Results will be announced later.");
            Console.WriteLine("  ══════════════════════════════════════════════════\n");
        }

        public override object Clone()
        {
            return new FinalExam(Time, Subject, new List<Question>(Questions));
        }

        public override string ToString() => $"[Final] {base.ToString()}";
    }
}