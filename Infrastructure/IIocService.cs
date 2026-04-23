using System;
using OoaipSpaceServer2026.Interfaces;

namespace OoaipSpaceServer2026.Infrastructure
{
    public interface IIocService
    {
        void Register<T>(string key, T implementation) where T : Delegate;
        T Resolve<T>(string key) where T : Delegate;
        object Resolve(string key, object arg);
    }
}