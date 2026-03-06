using System;
using System.Collections.Generic;
using ExamSystem.Enums;
using ExamSystem.Events;

namespace ExamSystem.Models
{
    public abstract class Exam : ICloneable, IComparable<Exam>
    {
        public int Time { get; protected set; }           // minutes
        public int NumberOfQuestions { get; protected set; }
        public List<Question> Questions { get; protected set; }
        public Dictionary<Question, Answer> QuestionAnswerDictionary { get; protected set; }
        public Subject Subject { get; protected set; }

        private ExamMode _mode;
        public ExamMode Mode
        {
            get => _mode;
            protected set
            {
                _mode = value;
                if (_mode == ExamMode.Starting)
                    RaiseExamStarted();
            }
        }

        public event ExamStartedHandler? ExamStarted;

        protected Exam(int time, Subject subject, List<Question> questions)
        {
            if (time <= 0)
                throw new ArgumentException("Time must be positive.", nameof(time));

            if (subject == null)
                throw new ArgumentNullException(nameof(subject));

            if (questions == null || questions.Count == 0)
                throw new ArgumentException("Exam must have at least one question.", nameof(questions));

            Time = time;
            Subject = subject;
            Questions = questions;
            NumberOfQuestions = questions.Count;

            QuestionAnswerDictionary = new Dictionary<Question, Answer>();
            _mode = ExamMode.Queued;
        }

        public abstract void ShowExam();
        

        public virtual void Start()
        {
            Console.WriteLine($"\n{'=',60}");
            Console.WriteLine($"  Starting Exam — Subject: {Subject.Name} | Time: {Time} min");
            Console.WriteLine($"{'=',60}");

            Mode = ExamMode.Starting;  

            foreach (var question in Questions)
            {
                question.Display();

                Answer? selected = GetStudentAnswer(question);

                if (selected != null)
                    QuestionAnswerDictionary[question] = selected;
            }
        }

        public virtual void Finish()
        {
            Mode = ExamMode.Finished;
            ShowExam();
        }

        public int CorrectExam()
        {
            int totalMarks = 0;
            int earnedMarks = 0;

            foreach (var question in Questions)
            {
                totalMarks += question.Marks;

                if (QuestionAnswerDictionary.TryGetValue(question, out Answer? studentAnswer))
                {
                    if (question is ChooseAllQuestion chooseAll)
                    {
                        if (chooseAll.CheckAnswer(studentAnswer))
                            earnedMarks += question.Marks;
                    }
                    else if (question.CheckAnswer(studentAnswer))
                        earnedMarks += question.Marks;
                }
            }

            Console.WriteLine($"\n  ── Grade: {earnedMarks} / {totalMarks} ──");
            return earnedMarks;
        }

        protected Answer? GetStudentAnswer(Question question)
        {
            Console.Write("\n  Your answer (enter ID): ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Answer? ans = question.Answers.GetById(id);

                if (ans == null)
                    Console.WriteLine("  [Invalid answer ID — skipped]");

                return ans;
            }

            Console.WriteLine("  [Invalid input — skipped]");
            return null;
        }
        private void RaiseExamStarted()
        {
            ExamStarted?.Invoke(this, new ExamEventArgs(Subject, this));
        }

        public abstract object Clone();

        public int CompareTo(Exam? other)
        {
            if (other == null) return 1;

            int cmp = Time.CompareTo(other.Time);
            if (cmp != 0) return cmp;

            return NumberOfQuestions.CompareTo(other.NumberOfQuestions);
        }

        public override string ToString() =>
            $"Exam[{GetType().Name}] Subject={Subject.Name}, Time={Time}min, Questions={NumberOfQuestions}, Mode={Mode}";

        public override bool Equals(object? obj)
        {
            if (obj is Exam other)
            {
                return Subject.Name == other.Subject.Name &&
                       Time == other.Time &&
                       NumberOfQuestions == other.NumberOfQuestions;
            }

            return false;
        }

        public override int GetHashCode() =>
            HashCode.Combine(Subject.Name, Time, NumberOfQuestions);
    }
}