using System.Text;

namespace FoundItBE.Helpers;

public static class CredentialsGenerator
{
    private static readonly Random _random = new Random();

    public static string GenerateRandomString(int length = 5)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var result = new StringBuilder(length);

        for (int i = 0; i < length; i++)
        {
            result.Append(chars[_random.Next(chars.Length)]);
        }

        return result.ToString();
    }

    public static string GeneratePassword(int length = 8)
    {
        if (length < 4)
        {
            throw new ArgumentException("Password length must be at least 4 to include all required character types.");
        }

        const string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string lowerCase = "abcdefghijklmnopqrstuvwxyz";
        const string digits = "0123456789";
        const string specialChars = "!@#$%^&*()-_=+[]{}|;:,.<>?";
        const string allChars = upperCase + lowerCase + digits + specialChars;

        var password = new StringBuilder(length);

        // Ensure at least one character of each required type
        password.Append(upperCase[_random.Next(upperCase.Length)]);
        password.Append(digits[_random.Next(digits.Length)]);
        password.Append(specialChars[_random.Next(specialChars.Length)]);
        password.Append(lowerCase[_random.Next(lowerCase.Length)]);

        // Fill the rest of the password with random characters
        for (int i = 4; i < length; i++)
        {
            password.Append(allChars[_random.Next(allChars.Length)]);
        }

        // Shuffle the password to avoid predictable patterns
        return ShuffleString(password.ToString());
    }

    private static string ShuffleString(string input)
    {
        var array = input.ToCharArray();
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);
            (array[i], array[j]) = (array[j], array[i]); // Swap characters
        }
        return new string(array);
    }
}
