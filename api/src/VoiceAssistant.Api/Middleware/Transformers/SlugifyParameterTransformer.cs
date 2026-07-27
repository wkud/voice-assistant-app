namespace VoiceAssistant.Api.Middleware.Transformers;

public class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        if (value is null)
        {
            return null;
        }

        var stringValue = value.ToString()!;
        Span<char> spanValue = stackalloc char[stringValue.Length];
        stringValue.CopyTo(spanValue);

        var firstCharLowercase = spanValue[0].ToString().ToLowerInvariant()[0];
        spanValue[0] = firstCharLowercase;

        return spanValue.ToString();
    }
}