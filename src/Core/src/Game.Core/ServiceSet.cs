using System;
using System.Collections.Generic;
using static Game.Core.Debug;

namespace Game.Core
{
    public static class ServiceSet
    {
        static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public static T Add<T>(T service)
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
            {
                Log($"Attempted to register service of type {type} twice.");
                _services.Remove(type);
            }
            _services.Add(type, service);
            return service;
        }

        public static void Remove<T>()
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
                _services.Remove(type);
            else Log($"Attempted to unregister service of type {type}, but no service of this type (or type and equality) is registered.");
        }

        public static bool Has<T>() => _services.ContainsKey(typeof(T));

        public static T Get<T>(bool failIfNotRegistered = true)
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
                return (T)_services[type];
            if (failIfNotRegistered)
                Log($"Attempted to get service service of type {type}, but no service of this type is registered.");
            return default(T);
        }
    }
}
