using System.Collections.Immutable;
using System.Security.Cryptography;
using System.Text;

namespace ShortLink.Helpers;

public static class CryptoHelper
{
    private static readonly ImmutableArray<char> Chars = [.. "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890!*".ToCharArray()];

    public static string GetRandomString(short length = 9)
    {
        var b = new byte[length];
        var rnd = RandomNumberGenerator.Create();
        rnd.GetBytes(b);
        var strBuilder = new StringBuilder();
        for (var i = 0; i < length; ++i)
        {
            strBuilder.Append(Chars[b[i] % Chars.Length]);
        }
        return strBuilder.ToString();
    }
}