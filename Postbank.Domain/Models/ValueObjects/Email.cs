namespace Postbank.Domain.Models.ValueObjects;

public record Email
{
    public Email(string value)
    {
        this.Value = value;
    }
    public string Value { get; }

    public static Email Create(string value)
    {
        return new Email(value);
    }
}
