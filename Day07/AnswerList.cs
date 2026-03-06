using System;

namespace ExamSystem.Models
{
    public class AnswerList
    {
        private List<Answer> answers = new List<Answer>();

        public int Count => answers.Count;

        public void Add(Answer answer)
        {
            if (answer == null)
                throw new ArgumentNullException(nameof(answer));

            answers.Add(answer);
        }

        public Answer? GetById(int id)
        {
            foreach (var answer in answers)
            {
                if (answer.Id == id)
                    return answer;
            }

            return null;
        }

        public Answer this[int index]
        {
            get
            {
                if (index < 0 || index >= answers.Count)
                    throw new IndexOutOfRangeException("Index out of range.");

                return answers[index];
            }
        }
    }
}
