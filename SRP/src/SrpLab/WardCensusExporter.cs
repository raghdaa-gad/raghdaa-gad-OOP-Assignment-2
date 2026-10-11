
namespace SrpLab;

public sealed class WardCensusExporter
{
    public string Export(
        IReadOnlyDictionary<int, string> bedPatients,
        IReadOnlyDictionary<int, int> vitalsScores)
    {
        var lines = new List<string> { "bed,patient,acuity" };

        foreach (var bed in bedPatients.Keys.OrderBy(x => x))
        {
            lines.Add($"{bed},{bedPatients[bed]},{vitalsScores[bed]}");
        }

        return string.Join('\n', lines);
    }
}
