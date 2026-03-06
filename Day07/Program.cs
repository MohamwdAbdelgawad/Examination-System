using System;
using System.Collections.Generic;
using ExamSystem.Enums;
using ExamSystem.Generics;
using ExamSystem.IO;
using ExamSystem.Models;

namespace ExamSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("EXAMINATION MANAGEMENT SYSTEM");

            // Create Subject 
            var subject = new Subject("Computer Science 101");

            // Create & Enroll Students 
            var s1 = new Student(1, "Alice Johnson");
            var s2 = new Student(2, "Bob Smith");
            var s3 = new Student(3, "Carol White");

            subject.Enroll(s1);
            subject.Enroll(s2);
            subject.Enroll(s3);

            // create Questions 

            // True/False
            var tfQ = new TrueFalseQuestion(
                header: "Q1",
                body: "The sky is blue.",
                marks: 2,
                correctAnswer: new Answer(1, "True")
            );

            // Choose One
            var chooseOneAnswers = new AnswerList();
            chooseOneAnswers.Add(new Answer(1, "Paris"));
            chooseOneAnswers.Add(new Answer(2, "London"));
            chooseOneAnswers.Add(new Answer(3, "Berlin"));
            chooseOneAnswers.Add(new Answer(4, "Madrid"));

            var chooseOneQ = new ChooseOneQuestion(
                header: "Q2",
                body: "What is the capital of France?",
                marks: 3,
                answers: chooseOneAnswers,
                correctAnswer: new Answer(1, "Paris")
            );

            // Choose All
            var chooseAllAnswers = new AnswerList();
            chooseAllAnswers.Add(new Answer(1, "C#"));
            chooseAllAnswers.Add(new Answer(2, "HTML"));
            chooseAllAnswers.Add(new Answer(3, "Java"));
            chooseAllAnswers.Add(new Answer(4, "CSS"));

            var chooseAllQ = new ChooseAllQuestion(
                header: "Q3",
                body: "Which of the following are programming languages?",
                marks: 4,
                answers: chooseAllAnswers,
                correctAnswers: new List<Answer>
                {
                    new Answer(1, "C#"),
                    new Answer(3, "Java")
                }
            );

            //Log Questions 
            var practiceQList = new QuestionList("practice_questions.log");
            var finalQList = new QuestionList("final_questions.log");

            practiceQList.Add(tfQ);
            practiceQList.Add(chooseOneQ);
            practiceQList.Add(chooseAllQ);

            finalQList.Add(tfQ);
            finalQList.Add(chooseOneQ);
            finalQList.Add(chooseAllQ);

            Console.WriteLine("\n Questions logged to files.");

            // create Exams 
            var questions = new List<Question>
            {
                tfQ,
                chooseOneQ,
                chooseAllQ
            };

            var practiceExam = new PracticeExam(30, subject, questions);
            var finalExam = new FinalExam(60, subject, questions);

            // Generic Repository ──────────────────────
            var examRepo = new Repository<Exam>();
            //List<Exam> examRepo = new List<Exam>();
            //examRepo.Add(practiceExam);
            //examRepo.Add(finalExam);
            //examRepo.Sort();

            examRepo.Add(practiceExam);
            examRepo.Add(finalExam);
            examRepo.Sort();

            Console.WriteLine($"\nRepository contains {examRepo.Count} exam(s).");

            //choose Exam Type
            Console.WriteLine("\nSelect Exam Type:");
            Console.WriteLine("1 - Practice Exam");
            Console.WriteLine("2 - Final Exam");

            Console.Write("Your choice: ");

            Exam? selectedExam = null;
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    selectedExam = practiceExam;
                    break;

                case "2":
                    selectedExam = finalExam;
                    break;

                default:
                    Console.WriteLine("Invalid selection. Defaulting to Practice.");
                    selectedExam = practiceExam;
                    break;
            }

            // start Exam
            subject.NotifyStudents(selectedExam);

            Console.WriteLine($"\nStarting: {selectedExam}");

            selectedExam.Start();
            selectedExam.Finish();


        }


    }
}