
namespace SrpLab;

public sealed class WardBoard
{
    private readonly Dictionary<int, string> _bedPatient = new();
    private readonly Dictionary<int, int> _vitalsScore = new();
    private readonly List<string> _pagerLog = new();

    private readonly AcuityScoreCalculator _acuityCalculator = new();
    private readonly PagerAlertPolicy _pagerPolicy = new();
    private readonly HandoffNoteFormatter _handoffFormatter = new();
    private readonly WardCensusExporter _censusExporter = new();

    public void AssignBed(int bed, string patientId, int heartRate, int spo2)
    {
        if (bed <= 0)
            throw new ArgumentOutOfRangeException(nameof(bed));

        if (string.IsNullOrWhiteSpace(patientId))
            throw new ArgumentException("patient required");

        _bedPatient[bed] = patientId.Trim().ToUpperInvariant();
        _vitalsScore[bed] = ScoreAcuity(heartRate, spo2);

        if (_pagerPolicy.ShouldAlert(_vitalsScore[bed]))
            _pagerLog.Add(_pagerPolicy.CreateAlert(bed));
    }

    public int ScoreAcuity(int heartRate, int spo2)
    {
        return _acuityCalculator.Calculate(heartRate, spo2);
    }

    public string BuildHandoffNote(int bed)
    {
        if (!_bedPatient.TryGetValue(bed, out var patient))
            return _handoffFormatter.FormatEmptyBed(bed);

        return _handoffFormatter.Format(
            bed,
            patient,
            _vitalsScore[bed]);
    }

    public IReadOnlyList<string> DrainPagerLog()
    {
        var copy = _pagerLog.ToList();
        _pagerLog.Clear();
        return copy;
    }

    public string ExportCensusCsv()
    {
        return _censusExporter.Export(_bedPatient, _vitalsScore);
    }
}
