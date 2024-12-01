using Bogus;
using Microsoft.EntityFrameworkCore;
using Postbank.Application.Seeder;
using Postbank.Domain.Models;
using Postbank.Domain.Models.ValueObjects;
using Postbank.Infrastructure.Persistance;

namespace Postbank.Infrastructure.Seeder;

internal class DatabaseSeeder : ISeeder
{
    private readonly ApplicationDbContext _dbContext;
    public DatabaseSeeder(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
        await _dbContext.Database.MigrateAsync();

        await _dbContext.TimeLogs.ExecuteDeleteAsync();
        await _dbContext.Users.ExecuteDeleteAsync();
        await _dbContext.Projects.ExecuteDeleteAsync();

        await GenerateUsersAsync();
        await GenerateProjectsAsync();
        await GenerateTimeLogsAsync();

    }

    private async Task GenerateUsersAsync()
    {
        for (int i = 0; i < 100; i++)
        {
            var firstName = new Faker().PickRandom("John", "Gringo", "Mark", "Lisa", "Maria", "Sonya", "Philip", "Jose", "Lorenzo", "George", "Justin");
            var lastName = new Faker().PickRandom("Johnson", "Lamas", "Jackson", "Brown", "Mason", "Rodriguez", "Roberts", "Thomas", "Rose", "McDonalds");
            var domain = new Faker().PickRandom("gmail.com", "hotmail.com", "live.com");

            var email = Email.Create($"{firstName}.{lastName}@{domain}");

            var user = User.Create(firstName, lastName, email);
            await _dbContext.Users.AddAsync(user);
        }

        await _dbContext.SaveChangesAsync();
    }

    private async Task GenerateProjectsAsync()
    {
        var projectOne = Project.Create("My own");
        var projectTwo = Project.Create("Free Time");
        var projectThree = Project.Create("Work");

        await _dbContext.Projects.AddRangeAsync(projectOne, projectTwo, projectThree);

        await _dbContext.SaveChangesAsync();
    }

    private async Task GenerateTimeLogsAsync()
    {
        var users = await _dbContext.Users.ToListAsync();
        foreach (var user in users)
        {
            var records = new Faker().Random.Number(1, 20);

            for (int i = 0; i < records; i++)
            {
                do
                {
                    var timeToLog = (float)Math.Round(new Faker().Random.Float(0.25f, 8.00f), 2);
                    var randomDate = new Faker().Date.Recent(30).ToUniversalTime();
                    var timeLogsForUser = await _dbContext.TimeLogs
                            .ToListAsync();

                    var loggedHours = timeLogsForUser
                        .Where(x => x.UserId == user.Id && x.Date.Day == randomDate.Day && x.Date.Month == randomDate.Month && x.Date.Year == randomDate.Year)
                        .Sum(x => x.Time.Value);

                    if (loggedHours + timeToLog <= 8)
                    {
                        var projectIds = await _dbContext.Projects.Select(x => x.Id).ToListAsync();
                        var projectId = new Faker().PickRandom(projectIds);

                        var timeLog = TimeLog.Create(user.Id, projectId, Time.Create(timeToLog), randomDate);

                        await _dbContext.TimeLogs.AddAsync(timeLog);
                        await _dbContext.SaveChangesAsync();

                        break;
                    }
                } while (true);
            }
        }
    }
}
