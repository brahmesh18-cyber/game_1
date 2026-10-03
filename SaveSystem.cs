using System;
using UnityEngine;

namespace EchoBound
{
    [Serializable]
    public class SaveData
    {
        public int level = 1;
        public int xp = 0;
        public string[] completedQuests = Array.Empty<string>();
    }

    public static class SaveSystem
    {
        private const string SaveKey = "ECHOBOUND_SAVE";

        public static void Save(SaveData data)
        {
            if (data == null) return;
            PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(data));
            PlayerPrefs.Save();
        }

        public static SaveData Load()
        {
            if (!PlayerPrefs.HasKey(SaveKey))
                return new SaveData();

            return JsonUtility.FromJson<SaveData>(
                PlayerPrefs.GetString(SaveKey));
        }

        public static void DeleteSave()
        {
            PlayerPrefs.DeleteKey(SaveKey);
            PlayerPrefs.Save();
        }
    }
}
