using System.ComponentModel.DataAnnotations;

namespace ShortLink.Attributes;

public class UrlValidAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null) return false;
        if (value.GetType() != typeof(string)) return false;
        var val = (string)value;
        return Uri.TryCreate(val, UriKind.Absolute, out var uriResult)
                      && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}