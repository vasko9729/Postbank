namespace Postbank.Web.Client.Requests;

public class UsersRequest
{
	public int CurrentPage { get; set; } = 1;

	public DateTime? From { get; set; } = default;
}
