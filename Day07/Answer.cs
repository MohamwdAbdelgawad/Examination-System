using System;

namespace ExamSystem.Models
{
    public class Answer : IComparable<Answer>
    {
        public int Id { get; }
        public string Text { get; }

        public Answer(int id, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Answer text cannot be null or empty.", nameof(text));
            if (id <= 0)
                throw new ArgumentException("Answer Id must be positive.", nameof(id));

            Id = id;
            Text = text;
        }

        public int CompareTo(Answer? other)
        {
            if (other == null) return 1;
            return Id.CompareTo(other.Id);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Answer other)
                return Id == other.Id;
            return false;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => $"[{Id}] {Text}";
    }
}
