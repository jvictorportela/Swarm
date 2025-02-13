using Swarm.Domain.Security.Criptography;
using System.Security.Cryptography;
using System.Text;

namespace Swarm.Infrastructure.Security.Criptography;
public class Shar512Encrypter : IPasswordEncrypter
{
    private readonly string _additionaKey;

    public Shar512Encrypter(string additionaKey)
    {
        _additionaKey = additionaKey;
    }

    public string Encrypt(string password)
    {
        var newPassword = $"{password}{_additionaKey}";

        var bytes = Encoding.UTF8.GetBytes(newPassword);
        var hashBytes = SHA512.HashData(bytes);

        return StringBytes(hashBytes);
    }

    private static string StringBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (byte b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }

        return sb.ToString();
    }
}

