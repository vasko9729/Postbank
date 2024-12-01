using Postbank.Domain.Models.ValueObjects;
using Postbank.Domain.Shared;

namespace Postbank.Domain.Models;

public class User : Entity
{
    private User()
    {

    }
    private User(string firstName, string lastName, Email email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public Email Email { get; private set; }

    public ICollection<TimeLog> TimeLogs { get; private set; } = new HashSet<TimeLog>();
    public static User Create(string firstName, string lastName, Email email)
    {
        return new User(firstName, lastName, email);
    }
}
