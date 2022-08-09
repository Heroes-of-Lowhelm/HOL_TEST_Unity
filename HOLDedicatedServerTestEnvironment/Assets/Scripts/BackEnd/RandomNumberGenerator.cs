
using System.Security.Cryptography;
 public class RandomIdGenerator 
    {
        const string LOWER_CASE = "abcdefghijklmnopqursuvwxyz";
        const string UPPER_CASE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string NUMBERS = "0123456789";
        const string SPECIALS = @"!@$%^&*";


        public static string GenerateID(bool useLowercase = true, bool useUppercase = true, bool useNumbers = true, bool useSpecial = false, int idSize = 24)
        {
            char[] _password = new char[idSize];
            string charSet = ""; // Initialise to blank
            var _random = RandomNumberGenerator.Create();
            int counter = 0;

            // Build up the character set to choose from
            if (useLowercase)
            {
                charSet += LOWER_CASE;
                counter = Set(_password, LOWER_CASE, counter, _random);
            }

            if (useUppercase)
            {
                charSet += UPPER_CASE;
                counter = Set(_password, UPPER_CASE, counter, _random);
            }

            if (useNumbers)
            {
                charSet += NUMBERS;
                counter = Set(_password, NUMBERS, counter, _random);
            }

            if (useSpecial)
            {
                charSet += SPECIALS;
                counter = Set(_password, SPECIALS, counter, _random);
            }

            for (int x = counter; x < idSize; x++)
            {
                _password[x] = charSet[_random.Next(0, charSet.Length - 1)];
            }

            _random.Shuffle(_password);

            return string.Join(null, _password);
        }

        private static int Set(char[] currentPassword, string toAdd, int counter, RandomNumberGenerator random)
        {
            currentPassword[counter] = toAdd[random.Next(0, toAdd.Length - 1)];
            counter++;
            return counter;
        }
    }

