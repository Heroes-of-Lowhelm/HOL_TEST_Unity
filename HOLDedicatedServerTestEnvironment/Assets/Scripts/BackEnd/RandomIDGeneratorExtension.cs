using System;
using System.Security.Cryptography;


public static class RandomIDGeneratorExtension
{
    public static int Next(this RandomNumberGenerator generator, int min, int max)
    {
        // match Next of Random
        // where max is exclusive
        max = max - 1;

        var bytes = new byte[sizeof(int)]; // 4 bytes
        generator.GetNonZeroBytes(bytes);
        var val = BitConverter.ToInt32(bytes, 0);

        var result = ((val - min) % (max - min + 1) + (max - min + 1)) % (max - min + 1) + min;
        return result;
    }

    public static void Shuffle<T>(this RandomNumberGenerator rng, T[] array)
    {
        int n = array.Length - 1;
        while (n > 1)
        {
            int k = rng.Next(0, array.Length - 1);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
            n--;
        }
    }
}

