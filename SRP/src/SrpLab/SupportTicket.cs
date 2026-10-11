namespace SrpLab;

public sealed class SupportTicket
{
    private readonly TicketPriorityClassifier _priorityClassifier = new();
    private readonly TicketSlaPolicy _slaPolicy = new();
    private readonly TicketReplyFormatter _replyFormatter = new();
    private readonly TicketEscalationFormatter _escalationFormatter = new();

    public string Id { get; }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public DateTimeOffset OpenedAt { get; }
    public string Priority { get; private set; } = "P3";

    public SupportTicket(
        string id,
        string subject,
        string body,
        DateTimeOffset openedAt)
    {
        Id = id;
        Subject = subject;
        Body = body;
        OpenedAt = openedAt;

        RecalculatePriorityFromText();
    }

    public void AppendCustomerMessage(string text)
    {
        Body += "\n---\n" + text;
        RecalculatePriorityFromText();
    }

    public void RecalculatePriorityFromText()
    {
        Priority = _priorityClassifier.Classify(Subject, Body);
    }

    public DateTimeOffset SlaDeadline()
    {
        return _slaPolicy.GetDeadline(OpenedAt, Priority);
    }

    public bool IsBreached(DateTimeOffset now)
    {
        return _slaPolicy.IsBreached(now, SlaDeadline());
    }

    public string DraftPublicReply(string agentName)
    {
        return _replyFormatter.Format(
            Id,
            agentName,
            Priority,
            SlaDeadline());
    }

    public string InternalEscalationBlurb()
    {
        return _escalationFormatter.Format(
            Id,
            Priority,
            SlaDeadline());
    }
}