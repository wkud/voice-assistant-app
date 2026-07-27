namespace VoiceAssistant.Domain.ValueObjects;

public record Amount
{
    /// <summary>
    /// Numeric value of the <see cref="Amount"/> expressed in <see cref="Unit"/>
    /// </summary>
    public decimal Value { get; private init; }

    /// <summary>
    /// Unit of measurement used to describe the <see cref="Amount"/>
    /// <example>"l" (for liter)</example>
    /// <example>"kg" (for kilogram)</example>
    /// <example>"g" (for gram)</example>
    /// <example>"ml" (for milliliter)</example>
    /// </summary>
    public string Unit { get; private init; }

    public Amount(decimal value, string unit)
    {
        Value = value;
        Unit = unit;
    }

    public static Amount InLiters(decimal amount) => new(amount, "l");
    public static Amount InMilliliters(decimal amount) => new(amount, "ml");
    public static Amount InKilograms(decimal amount) => new(amount, "kg");
    public static Amount InGrams(decimal amount) => new(amount, "g");

    public static Amount OneLiter = new(1m, "l");
    public static Amount HalfLiter = new(500m, "ml");
    public static Amount HundredMilliliters = new(100m, "ml");
    public static Amount OneMilliliter = new(1m, "ml");

    public static Amount OneKilogram = new(1m, "kg");
    public static Amount HalfKilogram = new(500m, "g");
    public static Amount HundredGrams = new(100m, "g");
    public static Amount OneGram = new(1m, "g");

    public static Amount? operator *(Amount a, decimal scalar)
    {
        return new Amount(a.Value * scalar, a.Unit);
    }

    public static Amount operator +(Amount a, decimal scalar)
    {
        return new Amount(a.Value + scalar, a.Unit);
    }

    public static Amount operator +(Amount a, Amount b)
    {
        if (!IsSameUnitFamily(a, b))
        {
            throw new InvalidOperationException($"Adding two Amount objects with units '{a.Unit}' and '{b.Unit}' is not supported");
        }

        var normalizedA = NormalizeToSiUnit(a);
        var normalizedB = NormalizeToSiUnit(a);
        return new Amount(normalizedA.Value + normalizedB.Value, normalizedA.Unit);
    }

    private static bool IsSameUnitFamily(Amount a, Amount b)
    {
        if (a.Unit == b.Unit)
        {
            return true;
        }

        return a.Unit switch
        {
            "l" or "ml" => b.Unit is "l" or "ml",
            "kg" or "g" => b.Unit is "kg" or "g",
            _ => throw new NotImplementedException($"Unit {a.Unit} or {b.Unit} is not supported for {nameof(IsSameUnitFamily)} method")
        };
    }

    private static Amount NormalizeToSiUnit(Amount a)
    {
        return a.Unit switch
        {
            "l" or "kg" => a,
            "g" => Amount.InKilograms(a.Value / 1000m),
            "ml" => Amount.InLiters(a.Value / 1000m),
            _ => throw new NotImplementedException($"Unit {a.Unit} is not supported for {nameof(NormalizeToSiUnit)} method")
        };
    }
}