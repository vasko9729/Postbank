using Postbank.Domain.Shared;

namespace Postbank.Domain.Models;

public class Project : Entity
{
    private Project()
    {

    }
    private Project(string name)
    {
        Name = name;
    }
    public string Name { get; private set; }

    public ICollection<TimeLog> TimeLogs { get; private set; } = new HashSet<TimeLog>();

    public static Project Create(string name)
    {
        return new Project(name);
    }
}
