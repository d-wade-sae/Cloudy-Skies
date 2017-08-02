using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager {


    public static void SavePlayerOne(PlayerStats playerstats)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/playerone.sav", FileMode.Create);

        PlayerData data = new PlayerData(playerstats);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static float[] LoadPlayerStatsOne()
    {
        if (File.Exists(Application.persistentDataPath + "/playerone.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/playerone.sav", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();
            return data.stats;
        }
        else
        {
            Debug.LogError("Stats does not exist.");
            return new float[10];
        }
    }

    public static int[] LoadPlayerLevelsOne()
    {
        if (File.Exists(Application.persistentDataPath + "/playerone.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/playerone.sav", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();
            return data.levels;
        }
        else
        {
            Debug.LogError("Levels does not exist.");
            return new int[2];
        }
    }

    public static void SavePlayerTwo(PlayerStats playerstats)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/playertwo.sav", FileMode.Create);

        PlayerData data = new PlayerData(playerstats);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static float[] LoadPlayerStatsTwo()
    {
        if (File.Exists(Application.persistentDataPath + "/playertwo.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/playertwo.sav", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();
            return data.stats;
        }
        else
        {
            Debug.LogError("Stats does not exist.");
            return new float[10];
        }
    }

    public static int[] LoadPlayerLevelsTwo()
    {
        if (File.Exists(Application.persistentDataPath + "/playertwo.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/playertwo.sav", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();
            return data.levels;
        }
        else
        {
            Debug.LogError("Levels does not exist.");
            return new int[2];
        }
    }

    public static void SavePlayerThree(PlayerStats playerstats)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/playerthree.sav", FileMode.Create);

        PlayerData data = new PlayerData(playerstats);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static float[] LoadPlayerStatsThree()
    {
        if (File.Exists(Application.persistentDataPath + "/playerthree.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/playerthree.sav", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();
            return data.stats;
        }
        else
        {
            Debug.LogError("Stats does not exist.");
            return new float[10];
        }
    }

    public static int[] LoadPlayerLevelsThree()
    {
        if (File.Exists(Application.persistentDataPath + "/playerthree.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/playerthree.sav", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();
            return data.levels;
        }
        else
        {
            Debug.LogError("Levels does not exist.");
            return new int[2];
        }
    }
}

[Serializable]
public class PlayerData
{
    public float[] stats;
    public int[] levels;

    public PlayerData(PlayerStats playerstats)







    {
        stats = new float[10];
        stats[0] = playerstats.playerMaxHealth;
        stats[1] = playerstats.playerHealth;
        stats[2] = playerstats.stamina;
        stats[3] = playerstats.maxStamina;
        stats[4] = playerstats.agility;
        stats[5] = playerstats.strength;
        stats[6] = playerstats.maxDefence;
        stats[7] = playerstats.currentDefence;
        stats[8] = playerstats.playerPosX;
        stats[9] = playerstats.playerPosY;

        levels = new int[2];
        levels[0] = playerstats.playerLevel;
        levels[1] = playerstats.currentXP;
    }

}
