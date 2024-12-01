using Postbank.Domain.Models.ValueObjects;
using Postbank.Domain.Shared;

namespace Postbank.Domain.Models;

public class TimeLog : Entity
{
    private TimeLog()
    {
        
    }
    private TimeLog(string userId, string projectId, Time time, DateTime date)
    {
        this.UserId = userId;
        this.ProjectId = projectId;
        this.Time = time;
        this.Date = date;
    }
    
    public string UserId { get; private set; }
    public User User { get; private set; }
    public string ProjectId { get; private set; }
    public Project Project { get; private set; }
    public Time Time { get; private set; }

    public DateTime Date {  get; private set; }

    public static TimeLog Create(string userId, string projectId, Time time, DateTime date)
    {
        return new TimeLog(userId, projectId, time, date);
    }

}
