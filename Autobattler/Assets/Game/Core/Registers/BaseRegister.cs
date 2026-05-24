using System.Collections.Generic;

namespace Game.Core.Registers
{
    public class BaseRegister<TKey, TValue>
    {
        protected Dictionary<TKey, TValue> Register = new();
        
        public bool TryAdd(TKey key, TValue value)
        {
            if (!Register.TryAdd(key, value))
                return false;

            return true;
        }

        public bool TryRemove(TKey key)
        {
            if (!Register.Remove(key))
                return false;

            return true;
        }
        
        public bool ContainsKey(TKey key)
        {
            return Register.ContainsKey(key);
        }
        
        public bool TryGetValue(TKey key, out TValue value)
        {
            return Register.TryGetValue(key, out value);
        }
    }
}