public class EnrollmentService
{
  public EnrollmentRecord ProcessRegistration(Student? student, Course? course)
  {
    // TODO 1: Guard Clauses — first give

    // student null then exist
    if (student is null)
      throw new ArgumentNullException(nameof(student));
    //   ↑                ↑
    //   Guard            nameof(student) = "student" string assign

    // course null exists now
    if (course is null)
      throw new ArgumentNullException(nameof(course));

    // the course is full then exist now
    if (course.EnrolledCount >= course.Capacity)
      throw new InvalidOperationException(
          $"Course {course.Code} is full ({course.EnrolledCount}/{course.Capacity}).");

    // TODO 2: Switch Expression — GPA Classification

    string standing = student.GPA switch
    {
      >= 3.5m => "Honors",          // GPA above 3.5 
      >= 2.5m => "Good Standing",   // GPA above 2.5 
      _ => "Academic Warning" //below (< 2.5)
    };

    Console.WriteLine($"{student.Name} is in {standing}.");

    // TODO 3: Return EnrollmentRecord

    return new EnrollmentRecord(student.Id, course.Code, DateTime.UtcNow);
  }
}