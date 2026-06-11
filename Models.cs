// Immutable by design - the logging pipeline cannot corrupt this
public record EnrollmentRecord(string StudentId, string CourseCode, DateTime ProcessedAt);

public class Course
{
  public required string Code { get; init; }
  public required string Title
  {
    get;
    set => field = !string.IsNullOrWhiteSpace(value)
    ? value
    : throw new ArgumentException("Title cannot be empty or whitespace.", nameof(value));
  }
  // C# 14Auto-property validation using 'field'
  public int Capacity
  {
    get;
    set => field = value > 0
    ? value
    : throw new ArgumentOutOfRangeException(nameof(value), "System constraint: Capacity must be greater than zero.");
  }
  public int EnrolledCount { get; set; }
}

// ex3-p3

public class Student
{
  public required string Id { get; init; }
  public required string Name
  {
    get;
    set => field = !string.IsNullOrWhiteSpace(value)
    ? value
    : throw new ArgumentException("Name cannot be empty or whitespace.", nameof(value));
  }
  public int Age
  {
    get;
    set => field = value is >= 16 and <= 100
    ? value
    : throw new ArgumentOutOfRangeException(nameof(value), "Age must be between 16 and 100.");
  }
  public decimal GPA
  {
    get;
    set => field = value is >= 0.0m and <= 4.0m
    ? value
    : throw new ArgumentOutOfRangeException(nameof(value), "GPA must be between 0.0and 4.0.");
  }
}
// ex 3B

public interface IGradable
{
  string Title { get; }
  decimal Calculation();
}
public class Quiz : IGradable
{
  public required string Title { get; init; }
  public required int CorrectAnswers { get; init; }
  public required int TotalQuestions { get; init; }

  public decimal Calculation()
  {
    if (TotalQuestions == 0) return 0m;
    return (decimal)CorrectAnswers / TotalQuestions * 100m;
  }
}
public class Labassignment : IGradable
{
  public required string Title { get; init; }
  public required decimal FunctionalityScore { get; init; }
  public required decimal CodeQualityScore { get; init; }

  public decimal Calculation()
  {
    // 70% functionality, 30% code quality
    return (FunctionalityScore * 0.7m) + (CodeQualityScore * 0.3m);
  }
}
