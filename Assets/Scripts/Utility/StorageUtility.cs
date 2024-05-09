using System;
using System.Collections.Generic;
using System.IO;
using Framework;
using UnityEngine;

namespace Elvenwood
{
    public interface IStorage : IUtility
    {
        void SaveInt(string key, int value);
        int LoadInt(string key, int defaultValue = 0);
        
        void SaveBool(string key, bool value);
        bool LoadBool(string key, bool defaultValue = false);

        
        void SaveByJson(string saveFileName, object data);
        T LoadFromJson<T>(string saveFileName);
        void DeleteSaveFile(string saveFileName);

    }

    public class StorageUtility : IStorage
    {
        #region PlayerPrefs
        public void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public int LoadInt(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        public void SaveBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        public bool LoadBool(string key, bool defaultValue = false)
        {
            return PlayerPrefs.GetInt(key, !defaultValue ? 0 : 1) == 1 ? true : false;
        }
        #endregion
        
        #region JSON

        public void SaveByJson(string saveFileName, object data)
        {
            var json = JsonUtility.ToJson(data);
            var path = Path.Combine(Application.persistentDataPath, saveFileName);

            try
            {     
                File.WriteAllText(path, json);

                #if UNITY_EDITOR
                Debug.Log($"Susscessfully saved data to {path}.");
                #endif
            }
            catch (Exception exception)
            {
                #if UNITY_EDITOR
                Debug.LogError($"Failed to save data to {path}. \n{exception}");
                #endif
            }
        }

        public T LoadFromJson<T>(string saveFileName)
        {
            var path = Path.Combine(Application.persistentDataPath, saveFileName);
            
            try
            {
                var json = File.ReadAllText(path);
                var data = JsonUtility.FromJson<T>(json);

                return data;
            }
            catch (Exception exception)
            {
                #if UNITY_EDITOR
                Debug.LogError($"Failed to load data from {path}. \n{exception}");
                #endif

                return default;
            }
        }

        #endregion

        
        #region Deleting

        public void DeleteSaveFile(string saveFileName)
        {
            var path = Path.Combine(Application.persistentDataPath, saveFileName);

            try
            {
                File.Delete(path); 
            }
            catch (Exception exception)
            {
                #if UNITY_EDITOR
                Debug.LogError($"Failed to delete {path}. \n{exception}");
                #endif
            }
        }

        #endregion
    }
    
    #region SerializationExtension
    
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver
    {
        [SerializeField] private List<TKey> keys = new List<TKey>();
        [SerializeField] private List<TValue> values = new List<TValue>();

        private Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();

            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            dictionary = new Dictionary<TKey, TValue>();

            if (keys.Count != values.Count)
                throw new Exception("键与值的数量不符");

            for (int i = 0; i < keys.Count; i++)
            {
                if (dictionary.ContainsKey(keys[i]))
                    continue; 

                dictionary[keys[i]] = values[i];
            }
        }

        public void Add(TKey key, TValue value)
        {
            dictionary.Add(key, value);
        }

        public bool Remove(TKey key)
        {
            return dictionary.Remove(key);
        }
        
        public TValue this[TKey key]
        {
            get { return dictionary[key]; }
            set { dictionary[key] = value; }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        public int Count
        {
            get { return dictionary.Count; }
        }
        
        public void Clear()
        {
            dictionary.Clear();
        }
        
        public bool ContainsKey(TKey key)
        {
            return dictionary.ContainsKey(key);
        }
        
    }
        
    #endregion
}