using System;
using ExamSystem.Events;

namespace ExamSystem.Models
{
    public class Student
    {
        public string Name { get; }
        public int Id { get; }

        public Student(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Student name cannot be empty.", nameof(name));
            if (id <= 0)
                throw new ArgumentException("Student Id must be positive.", nameof(id));

            Id   = id;
            Name = name;
        }

        public void OnExamStarted(object sender, ExamEventArgs e)
        {
            Console.WriteLine($" Student '{Name}' (ID: {Id}) — " +
                              $"Exam starting for subject '{e.Subject.Name}': {e.Exam}");
        }

        public override string ToString() => $"Student({Id}, {Name})";
        public override bool Equals(object? obj) => obj is Student s && Id == s.Id;
        public override int GetHashCode() => Id.GetHashCode();
    }
}
