using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tools
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [field: SerializeField] public List<Item> SelectedItems { get; private set; }

        public Dictionary<TKey, TValue> Dictionary { get; private set; } = new();

        public void OnBeforeSerialize()
        {
            if (Dictionary.Count <= 0)
                return;

            SelectedItems.Clear();
            
            foreach (var pair in Dictionary)
            {
                SelectedItems.Add(new Item { key = pair.Key, value = pair.Value });
            }
        }

        public void OnAfterDeserialize()
        {
            Dictionary.Clear();
            
            foreach (var entry in SelectedItems)
            {
                if (!Dictionary.TryAdd(entry.key, entry.value))
                {
                    Debug.LogError($"Item {entry.GetType()} is already in dictionary");
                }
            }
        }

        [Serializable]
        public class Item
        {
            public TKey key;
            public TValue value;
        }
    }
}