
namespace SrpLab;

public sealed class GradeBook
{
    private readonly GradeCalculator _calculator = new();
    private readonly LetterGradePolicy _letterPolicy = new();
    private readonly HonorRollPolicy _honorPolicy = new();
    private readonly TranscriptFormatter _transcriptFormatter = new();
    private readonly GradeCsvExporter _csvExporter = new();

    public void Record(string studentId, decimal score)
    {
        _calculator.Record(studentId, score);
    }

    public decimal Average(string studentId)
    {
        return _calculator.Average(studentId);
    }

    public string Letter(string studentId)
    {
        return _letterPolicy.GetLetter(Average(studentId));
    }

    public bool MeetsHonorRoll(string studentId)
    {
        return _honorPolicy.MeetsHonorRoll(
            Average(studentId),
            Letter(studentId));
    }

    public string TranscriptPlain(string studentId, string fullName)
    {
        return _transcriptFormatter.Format(
            studentId,
            fullName,
            Average(studentId),
            Letter(studentId),
            MeetsHonorRoll(studentId));
    }

    public string ExportCsv()
    {
        var grades = _calculator.StudentIds.Select(id =>
            (
                Id: id,
                Average: Average(id),
                Letter: Letter(id),
                Honor: MeetsHonorRoll(id)
            ));

        return _csvExporter.Export(grades);
    }
}
