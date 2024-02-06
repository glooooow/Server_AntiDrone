using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace AntiDrone.Utils;

public class PasswordHasher
{
    private static string HashPassword(string value, string salt)
    {
        var valueBytes = KeyDerivation.Pbkdf2(
            password: value,
            salt: Encoding.UTF8.GetBytes(salt),
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: 10000,
            numBytesRequested: 256 / 8);

        return Convert.ToBase64String(valueBytes);
    }
    public static string HashPassword(string password)
    {
        var salt = GenerateSalt(); /* 키를 생성하고 */
        var hash = HashPassword(password, salt); /* 입력받은 값과 생성된 키를 가지고 암호화 */
        var result = $"{salt}.{hash}"; /* 키가 포함된 암호화 값 (키가 포함되어 있어야 로그인 등 비교 검사가 가능) */
        Console.WriteLine("hash result:{0}", result);
        return result;
    }

    private static bool Validate(string password, string salt, string hash) => HashPassword(password, salt) == hash;

    public static bool VerifyHashedPassword(string password, string storePassword) /* 입력 받은 값과 이미 암호화된 값에서 salt 와 hash 값을 분리하여 검증하는 방법 */
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentNullException(nameof(password));
        }

        if (string.IsNullOrEmpty(storePassword))
        {
            throw new ArgumentNullException(nameof(storePassword));
        }

        var parts = storePassword.Split('.');
        var salt = parts[0];
        var hash = parts[1];

        return Validate(password, salt, hash); /* 검증을 위해 입력값과 분리된 salt, hash 를 파라미터로 받는다. => 로그인 시 복호화 과정 불필요 */
    }

    private static string GenerateSalt() /* 암호화시 사용될 키를 생성 */
    {
        byte[] randomBytes = new byte[128 / 8]; /* 키의 형태 */
        using (var generator = RandomNumberGenerator.Create()) /* 랜덤값으로 생성하는 방식 */
        {
            generator.GetBytes(randomBytes); /* 랜덤값의 키가 생성된다. */
            return Convert.ToBase64String(randomBytes);
        }
    }
}