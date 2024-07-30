using System.ComponentModel.DataAnnotations;

namespace ShortLink.Attributes;

public class IsLatinAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null) return true;
        if (value.GetType() != typeof(string)) return false;
        var val = (string)value;
        return val.All(char.IsAscii);
    }
}