using System;
using System.Collections.Generic;
using System.IO;
using Pixel_Adventure_1.DataSTO;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameTool
{
    public class GameData : SingletonMonoBehavior<GameData>
    {
        public DataPersistence Data;
        public static string fileName = "/dataPersistence.json";
        public STOFruitsData StoFruitsData;

        protected override void Awake()
        {
            base.Awake();
            Application.targetFrameRate = 60;
            Load();
            Save();
        }
        
        public bool MusicFX
        {
            get => Data.MusicFX;
            set
            {
                Data.MusicFX = value;
                if (value)
                {
                    AudioManager.I.TurnOnMusic();
                }
                else
                {
                    AudioManager.I.TurnOffMusic();
                }
                Save();
            }
        }
        
        public bool SoundFX
        {
            get => Data.SoundFX;
            set
            {
                Data.SoundFX = value;
                if (value)
                {
                    AudioManager.I.TurnOnSound();
                }
                else
                {
                    AudioManager.I.TurnOffSound();
                }
                Save();
            }
        }

        public List<FruitData> ListFruitItemsData
        {
            get => Data.ListFruitItemsData;
        }

        public List<FruitData> ListFruitUse
        {
            get
            {
                List<FruitData> _list = new List<FruitData>();
                for (int i = 0; i < ListFruitItemsData.Count; i++)
                {
                    if (ListFruitItemsData[i].isUsed)
                    {
                        _list.Add(ListFruitItemsData[i]);
                    }
                }

                return _list;
            }
        }

        public List<FruitData> ListFruitUnUse
        {
            get
            {
                List<FruitData> _list = new List<FruitData>();
                for (int i = 0; i < ListFruitItemsData.Count; i++)
                {
                    if (!ListFruitItemsData[i].isUsed)
                    {
                        _list.Add(ListFruitItemsData[i]);
                    }
                }

                return _list;
            }
        }

        public int CurrentLevel
        {
            get => Data.CurrentLevel;
            set
            {
                Data.CurrentLevel = value;
                Debug.Log("CurrentLevel set to: " + Data.CurrentLevel);
                if (Data.CurrentLevel > 3)
                {
                    Data.CurrentLevel = 1;
                    Debug.Log("ddd: " + Data.CurrentLevel);
                }
                Save();
            }
        }

        [Serializable]
        public class DataPersistence
        {
            [Header("RESOURCES")] public List<FruitData> ListFruitItemsData = new List<FruitData>();

            [Header("SETTINGS")] public bool MusicFX = true;
            [Header("SETTINGS")] public bool SoundFX = true;
            [Header("SETTINGS")] public bool Vibration = true;
            
            [Header("LEVEL")] public int CurrentLevel = 1;
        }

        public void Save()
        {
            string json = JsonUtility.ToJson(Data);

            File.WriteAllText(Application.persistentDataPath + fileName, json);
        }

        public void Load()
        {
            string path = Application.persistentDataPath + fileName;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                DataPersistence data = JsonUtility.FromJson<DataPersistence>(json);

                Data = data;
                if (data.ListFruitItemsData.Count == 0)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        FruitData fr = new FruitData();
                        fr.ID = i;
                        fr.isUsed = i < 4;
                        data.ListFruitItemsData.Add(fr);
                    }
                }
            }
        }
        
        public void ClearData()
        {
            string json = JsonUtility.ToJson(new DataPersistence());

            File.WriteAllText(Application.persistentDataPath + fileName, json);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            Save();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            Save();
        }

        private void OnApplicationQuit()
        {
            Save();
        }
    }

    public enum eResourceType
    {
    }

    [Serializable]
    public class FruitData
    {
        public int ID;
        public bool isUsed;
    }

    public interface ITakeDamageable
    {
        public void TakeDamage(float damage);
    }
}