using System;
using System.Collections.Generic;

namespace OoaipSpaceServer2026.Infrastructure
{
    public static class Ioc
    {
        private static readonly Dictionary<string, Delegate> _services = new();
        private static readonly Dictionary<string, Dictionary<string, Delegate>> _scopes = new();
        private static string? _currentScope;

        public static void Register(string key, Delegate implementation)
        {
            if (implementation is null)
                throw new ArgumentNullException(nameof(implementation));
            
            var scope = _currentScope ?? "global";
            if (!_scopes.ContainsKey(scope))
                _scopes[scope] = new Dictionary<string, Delegate>();
            
            _scopes[scope][key] = implementation;
        }

        public static T Resolve<T>(string key) where T : Delegate
        {
            var scope = _currentScope ?? "global";
            if (!_scopes.TryGetValue(scope, out var services) || !services.TryGetValue(key, out var service))
                throw new InvalidOperationException($"Dependency '{key}' is not registered in scope '{scope}'.");
            return (T)service;
        }

        public static object Resolve(string key, object arg)
        {
            var scope = _currentScope ?? "global";
            if (!_scopes.TryGetValue(scope, out var services) || !services.TryGetValue(key, out var service))
                throw new InvalidOperationException($"Dependency '{key}' is not registered in scope '{scope}'.");
            return service.DynamicInvoke(arg) 
                ?? throw new InvalidOperationException($"Dependency '{key}' returned null.");
        }

        public static void SetCurrentScope(string? scopeName) => _currentScope = scopeName;
        public static string? GetCurrentScope() => _currentScope;
    }
}