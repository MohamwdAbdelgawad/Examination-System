# Examination Management System

A console-based C# application demonstrating advanced OOP principles.

## Project Structure

```
ExamSystem/
├── Enums/
│   └── ExamMode.cs              # Starting, Queued, Finished
├── Events/
│   └── ExamEvents.cs            # ExamStartedHandler delegate + ExamEventArgs
├── Generics/
│   └── Repository.cs            # Repository<T> with ICloneable + IComparable constraints
├── IO/
│   └── QuestionList.cs          # Array-backed list with StreamWriter file logging
├── Models/
│   ├── Answer.cs                # IComparable<Answer>, Equals/GetHashCode
│   ├── AnswerList.cs            # Internally uses Answer[]
│   ├── Question.cs              # Abstract base with Header, Body, Marks
│   ├── TrueFalseQuestion.cs     # Derived — 2 answers, single select
│   ├── ChooseOneQuestion.cs     # Derived — multiple answers, single select
│   ├── ChooseAllQuestion.cs     # Derived — multiple answers, multi select
│   ├── Exam.cs                  # Abstract base, ICloneable, IComparable<Exam>
│   ├── PracticeExam.cs          # Shows correct answers after finish
│   ├── FinalExam.cs             # Hides correct answers after finish
│   ├── Student.cs               # OnExamStarted event handler
│   └── Subject.cs               # Enrollment + event wiring
└── Program.cs                   # Entry point / demo
```

## Design Decisions

### 1. Arrays instead of List<T>
All internal collections use `T[]` with manual resize (doubling strategy), satisfying the constraint while keeping amortized O(1) add performance.

### 2. Inheritance & Polymorphism
- `Question` → `TrueFalseQuestion`, `ChooseOneQuestion`, `ChooseAllQuestion`
- `Exam` → `PracticeExam`, `FinalExam`
- `ShowExam()` and `CheckAnswer()` demonstrate runtime polymorphism.

### 3. Events & Delegates
`Exam.Mode` setter fires `ExamStarted` when set to `Starting`.  
`Subject.NotifyStudents()` subscribes each enrolled student's `OnExamStarted` handler before the exam starts — clean pub/sub separation.

### 4. Generics with Constraints
`Repository<T> where T : ICloneable, IComparable<T>` enforces cloneability and sortability. Used to store and sort `Exam` instances.

### 5. File I/O
`QuestionList` accepts a unique filename per instance via constructor. Every `Add()` call opens `StreamWriter` in append mode and logs question metadata. Each `QuestionList` writes to its own file.

### 6. IComparable / ICloneable
- `Answer` implements `IComparable<Answer>` by Id.
- `Exam` implements `ICloneable` (shallow clone) and `IComparable<Exam>` by Time then NumberOfQuestions.

### 7. ChooseAllQuestion
Stores a `CorrectAnswers[]` array. `CheckAnswers(Answer[])` sorts both arrays by Id then compares element-wise — correct set comparison regardless of selection order.

### 8. SOLID Principles
- **S** — Each class has a single clear responsibility.
- **O** — New exam types / question types extend existing abstractions.
- **L** — `PracticeExam` and `FinalExam` are substitutable for `Exam`.
- **I** — `ICloneable` and `IComparable` are minimal focused interfaces.
- **D** — `Exam` depends on `Subject` abstraction; event system decouples notification.

## How to Run

```bash
dotnet run --project ExamSystem/ExamSystem.csproj
```

Requires .NET 8 SDK.

## Sample Output

```
  ╔═══════════════════════════════════════════════════╗
  ║        EXAMINATION MANAGEMENT SYSTEM              ║
  ╚═══════════════════════════════════════════════════╝

── Enrolling Students ──
  ✔ Student 'Alice Johnson' enrolled in 'Computer Science 101'.
  ✔ Student 'Bob Smith' enrolled in 'Computer Science 101'.
  ✔ Student 'Carol White' enrolled in 'Computer Science 101'.

  ✔ Questions logged to 'practice_questions.log' and 'final_questions.log'.
  Repository contains 2 exam(s), sorted by time.

╔══════════════════════════════╗
║   Select Exam Type           ║
║   1 - Practice Exam          ║
║   2 - Final Exam             ║
╚══════════════════════════════╝
  Your choice: 1

  📢 [Notification] Student 'Alice Johnson' (ID: 1) — Exam starting...
  📢 [Notification] Student 'Bob Smith' (ID: 2) — Exam starting...
  📢 [Notification] Student 'Carol White' (ID: 3) — Exam starting...

  Q1: The sky is blue.  [2 mark(s)] [True/False]
     [1] True
     [2] False
  Your answer (enter ID): 1

  Q2: What is the capital of France?  [3 mark(s)] [Choose One]
     [1] Paris
     ...

  ══════════════  PRACTICE EXAM RESULTS  ══════════════
  Q: The sky is blue.
     Your answer : [1] True
     Correct     : [1] True
     Result      : ✔ Correct
  ...
  ── Grade: 5 / 9 ──
```

## Logged Question File (`practice_questions.log`)

```
[2025-01-01 10:00:00] [True/False] [Q1] The sky is blue. (2 marks)
  Body    : The sky is blue.
  Marks   : 2
  Correct : [1] True
------------------------------------------------------------
[2025-01-01 10:00:00] [Choose One] [Q2] What is the capital of France? (3 marks)
  ...
```
