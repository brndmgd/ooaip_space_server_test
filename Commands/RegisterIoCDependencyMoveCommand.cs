using System;
using System.Collections.Generic;
using OoaipSpaceServer2026.Interfaces;
using OoaipSpaceServer2026.Infrastructure;
using OoaipSpaceServer2026.Models;

namespace OoaipSpaceServer2026.Commands
{
    public class RegisterIoCDependencyMoveCommand : ICommand
    {
        public void Execute()
        {
            Ioc.Register("Commands.Move", new Func<object, ICommand>((gameObject) =>
            {
                var adapter = (IDictionary<string, object>)Ioc.Resolve("Adapters.IMovingObject", gameObject);
                
                var getPosition = (Func<Vector>)adapter["Position"];
                var getVelocity = (Func<Vector>)adapter["Velocity"];
                var setPosition = (Action<Vector>)adapter["SetPosition"];
                
                return new MoveCommand(getPosition, getVelocity, setPosition);
            }));
        }
    }
}