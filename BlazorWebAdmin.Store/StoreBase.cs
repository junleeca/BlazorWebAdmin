﻿namespace BlazorWebAdmin.Store
{
    public abstract class StoreBase
    {
        private Dictionary<string, object> baseStore;

        public void Commit<T>(string key, object v)
        {
            var fullName = $"{typeof(T).FullName}_{key}";
            baseStore.TryAdd(fullName, v);
        }

        public object Get<T>(string key)
        {
            if(baseStore.TryGetValue(key, out var v))
            {
                return v;
            }
            return default;
        }

        public event Action DataChangedEvent;
        protected void NotifyChanged()
        {
            DataChangedEvent?.Invoke();
        }
    }
}
