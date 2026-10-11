namespace SrpLab;

public sealed class PagerAlertPolicy
{
    public bool ShouldAlert(int acuity)
    {
        return acuity >= 8;
    }

    public string CreateAlert(int bed)
    {
        return $"CODE-YELLOW bed={bed} at {DateTime.UtcNow:HH:mm}";
    }
}