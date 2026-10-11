namespace SrpLab;

public sealed class AppointmentSlotFinder
{
    public DateTimeOffset? FindNextSlot(
        DateTimeOffset from,
        int searchHours,
        int slotMinutes,
        Func<DateTimeOffset, bool> isWithinHours,
        HashSet<DateTimeOffset> booked)
    {
        var cursor = Align(from, slotMinutes);
        var end = from.AddHours(searchHours);

        while (cursor < end)
        {
            if (isWithinHours(cursor) && !booked.Contains(cursor))
                return cursor;

            cursor = cursor.AddMinutes(slotMinutes);
        }

        return null;
    }

    private static DateTimeOffset Align(DateTimeOffset from, int slotMinutes)
    {
        var minutes = from.Minute - from.Minute % slotMinutes;
        return new DateTimeOffset(
            from.Year, from.Month, from.Day,
            from.Hour, minutes, 0, from.Offset);
    }
}