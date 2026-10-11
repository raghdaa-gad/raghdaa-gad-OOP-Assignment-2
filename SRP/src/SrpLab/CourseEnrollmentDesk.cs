
namespace SrpLab;

public sealed class CourseEnrollmentDesk
{
    private readonly CourseEnrollmentService _enrollmentService;
    private readonly WelcomePacketFormatter _welcomeFormatter = new();
    private readonly TuitionInvoiceFormatter _invoiceFormatter = new();

    public int Capacity { get; }
    public decimal Tuition { get; }
    public string CourseCode { get; }

    public CourseEnrollmentDesk(
        string courseCode,
        int capacity,
        decimal tuition)
    {
        CourseCode = courseCode;
        Capacity = capacity;
        Tuition = tuition;

        _enrollmentService = new CourseEnrollmentService(capacity);
    }

    public string Register(string studentEmail)
    {
        return _enrollmentService.Register(studentEmail);
    }

    public int WaitlistPosition(string studentEmail)
    {
        return _enrollmentService.WaitlistPosition(studentEmail);
    }

    public string WelcomePacketMarkdown(
        string studentEmail,
        string studentName)
    {
        var status = _enrollmentService.HasSeat(studentEmail)
            ? "confirmed seat"
            : $"waitlist #{WaitlistPosition(studentEmail)}";

        return _welcomeFormatter.Format(CourseCode, studentName, status);
    }

    public string TuitionInvoiceLine(string studentEmail)
    {
        return _invoiceFormatter.Format(
            CourseCode,
            Tuition,
            _enrollmentService.HasSeat(studentEmail));
    }

    public void PromoteFromWaitlist(int seats)
    {
        _enrollmentService.PromoteFromWaitlist(seats);
    }
}
