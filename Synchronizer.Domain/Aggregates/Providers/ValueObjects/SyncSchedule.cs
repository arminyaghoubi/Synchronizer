using Synchronizer.Domain.Common;

namespace Synchronizer.Domain.Aggregates.Providers.ValueObjects;

public sealed record SyncSchedule : ValueObject
{
    public string CronExpression { get; } = null!;
    public int MaxRetryCount { get; }

    private SyncSchedule() { }

    private SyncSchedule(string cron, int maxRetryCount)
    {
        CronExpression = cron;
        MaxRetryCount = maxRetryCount;
    }

    public static Result<SyncSchedule> Create(string cron, int maxRetryCount)
    {
        if (string.IsNullOrEmpty(cron))
            return Result<SyncSchedule>.Failure("Cron can not be empty");

        if (maxRetryCount < 0)
            return Result<SyncSchedule>.Failure("MaxRetryCount must be positive");

        SyncSchedule schedule = new(cron, maxRetryCount);
        return Result<SyncSchedule>.Success(schedule);
    }
}