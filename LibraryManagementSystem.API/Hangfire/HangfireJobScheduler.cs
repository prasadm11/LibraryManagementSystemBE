using Hangfire;
using LibraryManagementSystem.Core.Interfaces.Services;

namespace LibraryManagementSystem.API.Hangfire;

public class HangfireJobScheduler
{
    public static void RegisterJobs()
    {
        

        RecurringJob.AddOrUpdate<INotificationService>(
            "overdue-email-job",
            x => x.SendOverdueEmails(),
            Cron.Daily
        );
        RecurringJob.AddOrUpdate<INotificationService>(
            "due-soon-email-job",
            x => x.SendDueSoonEmails(),
            Cron.Daily
        );
    }
}