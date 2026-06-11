namespace TmsCore
{
  record Enrollment(string StudentId, string CourseCode, DateTime EnrolledAt);

  class Student
  {
    // Provide non-null defaults so these members are not treated as unset
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public int Age { get; init; }
    public decimal GPA { get; init; }

    public Student() { }

    public Student(string id, string name)
    {
      Id = id;
      this.Name = name;
    }


    //Console.WriteLine("Hello, World!");

    // string? region = null;

    // string? upperRegion = region?.ToUpper();
    // Console.WriteLine($"Region (conditional): {upperRegion}");

    // string displayRegion = region ?? "Unassigned";
    // Console.WriteLine($"Region (coalesced): {displayRegion}");

    // region ??= "Addis Ababa";
    // Console.WriteLine($"Region (assigned): {region}");

    // // step3  —DeclareYour First TMS Variables
    // string studentName = "Abeba";
    // string studentId = "STU-001";
    // int enrollmentCount = 3;
    // decimal grantAmount = 1999.99m; // 'm' suffix marks a decimal literal
    // DateTime enrolledAt = DateTime.UtcNow;
    // string? campusRegion = null; // Nullable string for optional campus region
    // Console.WriteLine($"Student: {studentName} ({studentId})");
    // Console.WriteLine($"Courses: {enrollmentCount}");
    // Console.WriteLine($"Grant: {grantAmount:F2}");
    // Console.WriteLine($"Enrolled: {enrolledAt:yyyy-MM-dd}");
    // Console.WriteLine($"Campus:{campusRegion ?? "Not assigned"}");

    // // Legacy implementation — the bug that caused the audit failure
    // double grantPerStudent = 1999.99;
    // double totalAllocation = grantPerStudent * 100_000;
    // Console.WriteLine($"Total allocated (double): {totalAllocation}");
    // fixed implementation using  exact decimal type for financial calculations

    //
    // decimal grantPerStudent = 1999.99m;
    // decimal totalAllocation = grantPerStudent * 100_000m;
    // Console.WriteLine($"Total allocated (decimal): {totalAllocation}");
    // Console.WriteLine($"Total allocated (formatted): {totalAllocation:F2}");

  }

  class Course
  {
    public string Code { get; init; } = string.Empty;

    private string title = string.Empty;
    public string Title
    {
      get => title;
      set => title = !string.IsNullOrWhiteSpace(value)
          ? value
          : throw new ArgumentException("Title cannot be empty.", nameof(Title));
    }

    private int capacity;
    public int Capacity
    {
      get => capacity;
      set
      {
        if (value < 0)
          throw new ArgumentOutOfRangeException(nameof(Capacity), "Capacity must be non-negative.");
        capacity = value;
      }
    }
  }

  interface IGradable
  {
    string Title { get; init; }
    decimal CalculateGrade();
  }

  class Quiz : IGradable
  {
    public string Title { get; init; } = string.Empty;
    public int CorrectAnswers { get; init; }
    public int TotalQuestions { get; init; }

    public decimal CalculateGrade()
    {
      if (TotalQuestions <= 0)
        return 0m;
      return (decimal)CorrectAnswers / TotalQuestions * 100m;
    }
  }

  class Labassignment : IGradable
  {
    public string Title { get; init; } = string.Empty;
    public decimal FunctionalityScore { get; init; }
    public decimal CodeQualityScore { get; init; }

    public decimal CalculateGrade()
    {
      return (FunctionalityScore + CodeQualityScore) / 2m;
    }
  }

  class Program
  {
    static void Main()
    {
      var enrollment = new Enrollment("STU-001", "CS-401", DateTime.UtcNow);
      Console.WriteLine(enrollment);
      var corrected = enrollment with { CourseCode = "CS-402" };
      Console.WriteLine(corrected);
      // Value equality — two records with the same data are equal
      var duplicate = new Enrollment("STU-001", "CS-401", enrollment.EnrolledAt);
      Console.WriteLine($"Same data? {enrollment == duplicate}"); // True

      //Exercise 3 - p2

      var course = new Course { Code = "CS-401", Title = "Advanced C#", Capacity = 30 };
      Console.WriteLine($"Course: {course.Title} (Capacity: {course.Capacity})");
      // Invalid capacity — should throw
      try
      {
        course.Capacity = -5;
      }
      catch (ArgumentOutOfRangeException ex)
      {
        Console.WriteLine($"Caught: {ex.Message}");
      }
      // Invalid title — should throw
      try
      {
        course.Title = "";
      }
      catch (ArgumentException ex)
      {
        Console.WriteLine($"Caught: {ex.Message}");
      }
      // ex3-p3
      var s = new Student("S1", "Abeba") { Age = 20, GPA = 3.8m };
      Console.WriteLine($"Student: {s.Name}, GPA: {s.GPA}");

      var cohortAssessments = new IGradable[]
      {
        new Quiz { Title = "C#Basics", CorrectAnswers = 18, TotalQuestions = 20 },
        new Labassignment{ Title = "Registration API", FunctionalityScore = 90m, CodeQualityScore = 85m }
      };
      PrintGradeReport(cohortAssessments);
    }

    static void PrintGradeReport(IEnumerable<IGradable> assessments)
    {
      Console.WriteLine("---Grade Report---");
      foreach (var item in assessments)
      {
        Console.WriteLine($"{item.Title}: {item.CalculateGrade():F2}%");
      }
    }
  }
}



