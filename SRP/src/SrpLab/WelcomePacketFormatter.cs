namespace SrpLab;

public sealed class WelcomePacketFormatter
{
    public string Format(
        string courseCode,
        string studentName,
        string status)
    {
        return $"# Welcome to {courseCode}\nHi {studentName},\n" +
               $"Your status: **{status}**.\n" +
               $"Bring a laptop. Discord onboarding link: " +
               $"https://example.invalid/{courseCode.ToLowerInvariant()}\n";
    }
}