using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using OoaipSpaceServer2026.Commands;
using OoaipSpaceServer2026.Infrastructure;
using OoaipSpaceServer2026.Interfaces; 
using OoaipSpaceServer2026.Models;

namespace Tests
{
    public class RegisterIoCDependencyMoveCommandTests
    {
        [Fact]
        public void Execute_RegistersMoveCommandFactory_DependencyCanBeResolved()
        {
            var adapterMock = new Mock<IDictionary<string, object>>();
            adapterMock.Setup(d => d["Position"]).Returns((Func<Vector>)(() => new Vector(10, 20)));
            adapterMock.Setup(d => d["Velocity"]).Returns((Func<Vector>)(() => new Vector(1, 2)));
            adapterMock.Setup(d => d["SetPosition"]).Returns((Action<Vector>)(_ => { }));

            Ioc.Register("Adapters.IMovingObject", (Func<object, IDictionary<string, object>>)(obj => adapterMock.Object));

            var command = new RegisterIoCDependencyMoveCommand();

            command.Execute();

            var factory = Ioc.Resolve<Func<object, ICommand>>("Commands.Move");
            Assert.NotNull(factory);

            var gameObject = new object();
            var result = factory(gameObject);
            Assert.IsType<MoveCommand>(result);

            adapterMock.Verify(a => a["Position"], Times.Once);
            adapterMock.Verify(a => a["Velocity"], Times.Once);
            adapterMock.Verify(a => a["SetPosition"], Times.Once);
        }
    }
}