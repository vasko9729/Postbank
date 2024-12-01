namespace Postbank.Domain.Models.ValueObjects;

public record Time
{
    private Time(float value)
    {
        this.Value = value;
    }
    public float Value { get;}

    public static Time Create(float value)
    {
        if (value < 0.25 || value > 8.00)
        {
            throw new ArgumentException("Value must be between 0.25 and 8.00");
        }
        return new Time(value);
    }
}