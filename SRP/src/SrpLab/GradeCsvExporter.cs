
namespace SrpLab;

public sealed class GradeCsvExporter
{
    public string Export(IEnumerable<(string Id, decimal Average, string Letter, bool Honor)> grades)
    {
        var rows = new List<string> { "studentId,average,letter,honor" };

        foreach (var grade in grades.OrderBy(g => g.Id))
        {
            rows.Add($"{grade.Id},{grade.Average},{grade.Letter},{(grade.Honor ? 1 : 0)}");
        }

        return string.Join('\n', rows);
    }
}
