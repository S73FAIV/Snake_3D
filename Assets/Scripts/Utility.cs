using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Random = System.Random;

public class Utility
{
    public static T[] ShuffleArray<T>(T[] array, int seed)
    {
        Random prng = new Random(seed);

        for (int i = 0; i < array.Length - 1; i++)
        {
            int randomIndex = prng.Next(i, array.Length);
            (array[randomIndex], array[i]) = (array[i], array[randomIndex]);
        }

        return array;
    }

    public static int Mod(int a, int n) => (a % n + n) % n;



    public static void SaveHighScores(HighScore save)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat");
        bf.Serialize(file, save);
        file.Close();
    }
    

    public static HighScore LoadHighScore()
    {
        HighScore data;
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
            data = (HighScore) bf.Deserialize(file);
            file.Close();
        }
        else
        {
            data = new HighScore();
        }
        return data;
    }
}