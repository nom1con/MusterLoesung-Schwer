using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


    public class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            // Generate a salt (16 bytes is typical)
            using RNGCryptoServiceProvider rng = new();
            byte[] salt = new byte[16];
            rng.GetBytes(salt);

            // Use PBKDF2 with SHA-256
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32); // 32-byte hash length

            // Combine salt and hash into a single byte array for storage
            byte[] hashBytes = new byte[48];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 32);

            // Convert to base64 to store it (or another format like hexadecimal)
            return Convert.ToBase64String(hashBytes);
        }

        private static readonly char[] UpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private static readonly char[] LowerCase = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        private static readonly char[] Digits = "0123456789".ToCharArray();
        private static readonly char[] SpecialCharacters = "!@#$%^&*()-_=+[]{}|;:,.<>?".ToCharArray();

        public static string GeneratePassword(int length = 12, bool includeSpecialChars = true)
        {
            // Combine character sets based on the options
            var characterSet = new StringBuilder();
            characterSet.Append(new string(UpperCase));
            characterSet.Append(new string(LowerCase));
            characterSet.Append(new string(Digits));

            if (includeSpecialChars)
            {
                characterSet.Append(new string(SpecialCharacters));
            }

            var random = new Random();
            var password = new char[length];

            for (int i = 0; i < length; i++)
            {
                password[i] = characterSet[random.Next(characterSet.Length)];
            }

            return new string(password);
        }
        private static Random _random = new();

        public static string GeneratePhoneNumber()
        {
            // Format: (XXX) XXX-XXXX
            string phoneNumber = string.Format("({0:D3}) {1:D3}-{2:D4}",
                _random.Next(1, 1000),  // Area code (1-999)
                _random.Next(100, 1000), // Prefix (100-999)
                _random.Next(1000, 10000)); // Line number (1000-9999)

            return phoneNumber;
        }

        public static DateTime GenerateDateOfBirth(int minAge = 18, int maxAge = 70)
        {
            // Get today's date
            DateTime today = DateTime.Today;

            // Calculate the latest possible birthdate based on maxAge
            DateTime maxBirthDate = today.AddYears(-minAge);  // Min age, e.g., 18 years old
            DateTime minBirthDate = today.AddYears(-maxAge);  // Max age, e.g., 70 years old

            // Generate a random date between minBirthDate and maxBirthDate
            int range = (maxBirthDate - minBirthDate).Days;
            return minBirthDate.AddDays(_random.Next(range));
        }
    }

