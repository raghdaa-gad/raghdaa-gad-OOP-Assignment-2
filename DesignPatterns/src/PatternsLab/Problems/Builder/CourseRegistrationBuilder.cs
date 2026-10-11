
namespace PatternsLab.Problems.Builder;

public sealed class CourseRegistrationBuilder
{
    private string? _studentEmail;
    private string? _courseCode;
    private string? _accessMode;
    private string? _groupCode;
    private string? _discountCode;
    private bool _sendWhatsApp;
    private bool _sendEmailWelcome = true;
    private string? _mentorNote;
    private DateOnly? _preferredStart;

    public CourseRegistrationBuilder WithStudentEmail(string email)
    {
        _studentEmail = email;
        return this;
    }

    public CourseRegistrationBuilder WithCourseCode(string courseCode)
    {
        _courseCode = courseCode;
        return this;
    }

    public CourseRegistrationBuilder ForLiveGroup(string groupCode)
    {
        _accessMode = "LiveGroup";
        _groupCode = groupCode;
        return this;
    }

    public CourseRegistrationBuilder ForVideosOnly()
    {
        _accessMode = "VideosOnly";
        _groupCode = null;
        return this;
    }

    public CourseRegistrationBuilder WithDiscountCode(string? discountCode)
    {
        _discountCode = discountCode;
        return this;
    }

    public CourseRegistrationBuilder SendWhatsApp(bool send = true)
    {
        _sendWhatsApp = send;
        return this;
    }

    public CourseRegistrationBuilder SendEmailWelcome(bool send = true)
    {
        _sendEmailWelcome = send;
        return this;
    }

    public CourseRegistrationBuilder WithMentorNote(string? note)
    {
        _mentorNote = note;
        return this;
    }

    public CourseRegistrationBuilder WithPreferredStart(DateOnly? date)
    {
        _preferredStart = date;
        return this;
    }

    public CourseRegistration Build()
    {
        if (string.IsNullOrWhiteSpace(_studentEmail))
            throw new InvalidOperationException("Student email is required.");

        if (string.IsNullOrWhiteSpace(_courseCode))
            throw new InvalidOperationException("Course code is required.");

        if (_accessMode is null)
            throw new InvalidOperationException(
                "Access mode is required. Choose ForLiveGroup or ForVideosOnly.");

        if (_accessMode == "LiveGroup" &&
            string.IsNullOrWhiteSpace(_groupCode))
            throw new InvalidOperationException(
                "LiveGroup requires a GroupCode.");

        if (_accessMode == "VideosOnly" &&
            !string.IsNullOrWhiteSpace(_groupCode))
            throw new InvalidOperationException(
                "VideosOnly cannot have a GroupCode.");

        return new CourseRegistration(
            _studentEmail,
            _courseCode,
            _accessMode,
            _groupCode,
            _discountCode,
            _sendWhatsApp,
            _sendEmailWelcome,
            _mentorNote,
            _preferredStart);
    }
}
