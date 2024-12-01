namespace Postbank.Application.Users.Requests;

public class UsersRequest
{
    public int CurrentPage { get; set; } = 1;

    public DateTime? From { get; set; } = default;
}
