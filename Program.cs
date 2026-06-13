// namespace TmsCore



// {
//   record Enrollment(string StudentId, string CourseCode, DateTime EnrolledAt);

//   class Student
//   {
//     // Provide non-null defaults so these members are not treated as unset
//     public string Id { get; init; } = string.Empty;
//     public string Name { get; init; } = string.Empty;
//     public int Age { get; init; }
//     public decimal GPA { get; init; }

//     public Student() { }

//     public Student(string id, string name)
//     {
//       Id = id;
//       this.Name = name;
//     }


//     //Console.WriteLine("Hello, World!");

//     // string? region = null;

//     // string? upperRegion = region?.ToUpper();
//     // Console.WriteLine($"Region (conditional): {upperRegion}");

//     // string displayRegion = region ?? "Unassigned";
//     // Console.WriteLine($"Region (coalesced): {displayRegion}");

//     // region ??= "Addis Ababa";
//     // Console.WriteLine($"Region (assigned): {region}");

//     // // step3  —DeclareYour First TMS Variables
//     // string studentName = "Abeba";
//     // string studentId = "STU-001";
//     // int enrollmentCount = 3;
//     // decimal grantAmount = 1999.99m; // 'm' suffix marks a decimal literal
//     // DateTime enrolledAt = DateTime.UtcNow;
//     // string? campusRegion = null; // Nullable string for optional campus region
//     // Console.WriteLine($"Student: {studentName} ({studentId})");
//     // Console.WriteLine($"Courses: {enrollmentCount}");
//     // Console.WriteLine($"Grant: {grantAmount:F2}");
//     // Console.WriteLine($"Enrolled: {enrolledAt:yyyy-MM-dd}");
//     // Console.WriteLine($"Campus:{campusRegion ?? "Not assigned"}");

//     // // Legacy implementation — the bug that caused the audit failure
//     // double grantPerStudent = 1999.99;
//     // double totalAllocation = grantPerStudent * 100_000;
//     // Console.WriteLine($"Total allocated (double): {totalAllocation}");
//     // fixed implementation using  exact decimal type for financial calculations

//     //
//     // decimal grantPerStudent = 1999.99m;
//     // decimal totalAllocation = grantPerStudent * 100_000m;
//     // Console.WriteLine($"Total allocated (decimal): {totalAllocation}");
//     // Console.WriteLine($"Total allocated (formatted): {totalAllocation:F2}");

//   }

//   class Course
//   {
//     public string Code { get; init; } = string.Empty;

//     private string title = string.Empty;
//     public string Title
//     {
//       get => title;
//       set => title = !string.IsNullOrWhiteSpace(value)
//           ? value
//           : throw new ArgumentException("Title cannot be empty.", nameof(Title));
//     }

//     private int capacity;
//     public int Capacity
//     {
//       get => capacity;
//       set
//       {
//         if (value < 0)
//           throw new ArgumentOutOfRangeException(nameof(Capacity), "Capacity must be non-negative.");
//         capacity = value;
//       }
//     }

//     public int EnrolledCount { get; set; }
//   }

//   interface IGradable
//   {
//     string Title { get; init; }
//     decimal CalculateGrade();
//   }

//   class Quiz : IGradable
//   {
//     public string Title { get; init; } = string.Empty;
//     public int CorrectAnswers { get; init; }
//     public int TotalQuestions { get; init; }

//     public decimal CalculateGrade()
//     {
//       if (TotalQuestions <= 0)
//         return 0m;
//       return (decimal)CorrectAnswers / TotalQuestions * 100m;
//     }
//   }

//   class Labassignment : IGradable
//   {
//     public string Title { get; init; } = string.Empty;
//     public decimal FunctionalityScore { get; init; }
//     public decimal CodeQualityScore { get; init; }

//     public decimal CalculateGrade()
//     {
//       return (FunctionalityScore + CodeQualityScore) / 2m;
//     }
//   }

//   class Program
//   {
//     static void Main()
//     {
//       var enrollment = new Enrollment("STU-001", "CS-401", DateTime.UtcNow);
//       Console.WriteLine(enrollment);
//       var corrected = enrollment with { CourseCode = "CS-402" };
//       Console.WriteLine(corrected);
//       // Value equality — two records with the same data are equal
//       var duplicate = new Enrollment("STU-001", "CS-401", enrollment.EnrolledAt);
//       Console.WriteLine($"Same data? {enrollment == duplicate}"); // True

//       //Exercise 3 - p2

//       var course = new Course { Code = "CS-401", Title = "Advanced C#", Capacity = 30 };
//       Console.WriteLine($"Course: {course.Title} (Capacity: {course.Capacity})");
//       // Invalid capacity — should throw
//       try
//       {
//         course.Capacity = -5;
//       }
//       catch (ArgumentOutOfRangeException ex)
//       {
//         Console.WriteLine($"Caught: {ex.Message}");
//       }
//       // Invalid title — should throw
//       try
//       {
//         course.Title = "";
//       }
//       catch (ArgumentException ex)
//       {
//         Console.WriteLine($"Caught: {ex.Message}");
//       }
//       // ex3-p3
//       var s = new Student("S1", "Abeba") { Age = 20, GPA = 3.8m };
//       Console.WriteLine($"Student: {s.Name}, GPA: {s.GPA}");

//       var cohortAssessments = new IGradable[]
//       {
//         new Quiz { Title = "C#Basics", CorrectAnswers = 18, TotalQuestions = 20 },
//         new Labassignment{ Title = "Registration API", FunctionalityScore = 90m, CodeQualityScore = 85m }
//       };
//       PrintGradeReport(cohortAssessments);
//     }

//     static void PrintGradeReport(IEnumerable<IGradable> assessments)
//     {
//       Console.WriteLine("---Grade Report---");
//       foreach (var item in assessments)
//       {
//         Console.WriteLine($"{item.Title}: {item.CalculateGrade():F2}%");
//       }
//     }
//   }
// }

// M1-S2
var service = new EnrollmentService();

// Test 1: Valid registration — 
var validStudent = new Student { Id = "S1", Name = "Abeba", Age = 20, GPA = 3.8m };
var validCourse = new Course { Code = "CS-401", Title = "Advanced C#", Capacity = 30 };
var result = service.ProcessRegistration(validStudent, validCourse);
Console.WriteLine($"Enrolled: {result.StudentId} in {result.CourseCode}");
// Output: Abeba is in Honors.
//         Enrolled: S1 in CS-401

// Test 2: Null student — null ተማሪ
try
{
  service.ProcessRegistration(null, validCourse);
}
catch (ArgumentNullException ex)
{
  Console.WriteLine($"Guard caught: {ex.ParamName}");
  // Output: Guard caught: student
}

// Test 3: Full course —
var fullCourse = new Course { Code = "CS-402", Title = "Full Course", Capacity = 1 };
fullCourse.EnrolledCount = 1;
try
{
  service.ProcessRegistration(validStudent, fullCourse);
}
catch (InvalidOperationException ex)
{
  Console.WriteLine($"Business rule: {ex.Message}");
  // Output: Business rule: Course CS-402 is full (1/1).
}



// C# 12+ Collection Expressions — 
List<Student> students = [
    new Student { Id = "S1", Name = "Abeba",    Age = 22, GPA = 3.8m },
    new Student { Id = "S2", Name = "Kidane",   Age = 21, GPA = 2.4m },
    new Student { Id = "S3", Name = "Dawit",    Age = 20, GPA = 3.1m },
    new Student { Id = "S4", Name = "Sara",     Age = 23, GPA = 3.9m },
    new Student { Id = "S5", Name = "Frehiwot", Age = 19, GPA = 2.0m },
    new Student { Id = "S6", Name = "Yonas",    Age = 24, GPA = 3.5m },
    new Student { Id = "S7", Name = "Meron",    Age = 22, GPA = 1.8m },
    new Student { Id = "S8", Name = "Tesfaye",  Age = 21, GPA = 2.9m }
];
var leaderboard = students
    .Where(s => s.GPA >= 3.5m)           // TODO 1: GPA >= 3.5 
    .OrderByDescending(s => s.GPA)        // TODO 2: GPA 
    .Select(s => s.Name)                  // TODO 3: Name 
    .ToList();                            // TODO 4: List 
Console.WriteLine($"Found {leaderboard.Count} Honors Students:");
foreach (var name in leaderboard)
{
  Console.WriteLine($"- {name}");
}
