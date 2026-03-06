using System;
using ExamSystem.Events;

namespace ExamSystem.Models
{
    public class Subject
    {
        public string Name { get; }
        private Student[] _enrolledStudents;
        private int _studentCount;
        private const int DefaultCapacity = 10;

        public Student[] EnrolledStudents
        {
            get
            {
                Student[] copy = new Student[_studentCount];
                Array.Copy(_enrolledStudents, copy, _studentCount);
                return copy;
            }
        }

        public Subject(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Subject name cannot be empty.", nameof(name));
            Name = name;
            _enrolledStudents = new Student[DefaultCapacity];
        }

        public void Enroll(Student student)
        {
            if (student == null) throw new ArgumentNullException(nameof(student));

            // Avoid duplicates
            for (int i = 0; i < _studentCount; i++)
                if (_enrolledStudents[i].Equals(student))
                    return;

            if (_studentCount == _enrolledStudents.Length)
                Resize();

            _enrolledStudents[_studentCount++] = student;
        }

        public void NotifyStudents(Exam exam)
        {
            if (exam == null) throw new ArgumentNullException(nameof(exam));
            for (int i = 0; i < _studentCount; i++)
                exam.ExamStarted += _enrolledStudents[i].OnExamStarted;
        }

        private void Resize()
        {
            Student[] bigger = new Student[_enrolledStudents.Length * 2];
            Array.Copy(_enrolledStudents, bigger, _studentCount);
            _enrolledStudents = bigger;
        }

        public override string ToString() => $"Subject({Name})";
    }
}
