using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = System.Random;

public class Utility
{
    public static T[] ShuffleArray<T>(T[] array, int seed)
    {
        System.Random prng = new Random(seed);

        for (int i = 0; i < array.Length - 1; i++)
        {
            int randomIndex = prng.Next(i, array.Length);
            (array[randomIndex], array[i]) = (array[i], array[randomIndex]);
        }

        return array;
    }

    public static int Mod(int a, int n) => (a % n + n) % n;
}