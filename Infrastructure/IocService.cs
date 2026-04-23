using System;
using System.Collections.Generic;

namespace OoaipSpaceServer2026.Infrastructure
{
    public class IocService : IIocService
    {
        private readonly Dictionary<string, Delegate> _services = new();

        public void Register<T>(string key, T implementation) where T : Delegate
        {
            if (implementation is null)
                throw new ArgumentNullException(nameof(implementation));
            _services[key] = implementation;
        }

        public T Resolve<T>(string key) where T : Delegate
        {
            if (!_services.TryGetValue(key, out var service))
                throw new InvalidOperationException($"Dependency '{key}' is not registered.");
            return (T)service;  
        }

        public object Resolve(string key, object arg)
        {
            if (!_services.TryGetValue(key, out var service))
                throw new InvalidOperationException($"Dependency '{key}' is not registered.");
            return service.DynamicInvoke(arg) 
                ?? throw new InvalidOperationException($"Dependency '{key}' returned null.");
        }
    }
}