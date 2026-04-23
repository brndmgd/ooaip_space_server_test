using System;
using System.Collections.Generic;
using OoaipSpaceServer2026.Interfaces;
using OoaipSpaceServer2026.Infrastructure;
using OoaipSpaceServer2026.Models;

namespace OoaipSpaceServer2026.Commands
{
    public class RegisterIoCDependencyMoveCommand : ICommand
    {
        private readonly IIocService _ioc;

        public RegisterIoCDependencyMoveCommand(IIocService ioc)
        {
            _ioc = ioc ?? throw new ArgumentNullException(nameof(ioc));
        }

        public void Execute()
        {
            _ioc.Register<Func<object, ICommand>>("Commands.Move", (gameObject) =>
            {
                // Получаем адаптер для игрового объекта через IoC
                var adapter = (IDictionary<string, object>)_ioc.Resolve("Adapters.IMovingObject", gameObject);
                
                // Извлекаем делегаты из адаптера
                var getPosition = (Func<Vector>)adapter["Position"];
                var getVelocity = (Func<Vector>)adapter["Velocity"];
                var setPosition = (Action<Vector>)adapter["SetPosition"];
                
                // Создаём и возвращаем команду перемещения
                return new MoveCommand(getPosition, getVelocity, setPosition);
            });
        }
    }
}