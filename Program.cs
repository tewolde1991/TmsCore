//Console.WriteLine("Hello, World!");

// string? region = null;

// string? upperRegion = region?.ToUpper();
// Console.WriteLine($"Region (conditional): {upperRegion}");

// string displayRegion = region ?? "Unassigned";
// Console.WriteLine($"Region (coalesced): {displayRegion}");

// region ??= "Addis Ababa";
// Console.WriteLine($"Region (assigned): {region}");

// // step3  —DeclareYour First TMS Variables
string studentName = "Abeba";
string studentId = "STU-001";
int enrollmentCount = 3;
decimal grantAmount = 1999.99m; // 'm' suffix marks a decimal literal
DateTime enrolledAt = DateTime.UtcNow;
string? campusRegion = null; // Nullable string for optional campus region
Console.WriteLine($"Student: {studentName} ({studentId})");
Console.WriteLine($"Courses: {enrollmentCount}");
Console.WriteLine($"Grant: {grantAmount:F2}");
Console.WriteLine($"Enrolled: {enrolledAt:yyyy-MM-dd}");
Console.WriteLine($"Campus:{campusRegion ?? "Not assigned"}");

