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
            var iocMock = new Mock<IIocService>();
            
            var adapter = new Dictionary<string, object>
            {
                ["Position"] = (Func<Vector>)(() => new Vector(1, 2)),
                ["Velocity"] = (Func<Vector>)(() => new Vector(3, 4)),
                ["SetPosition"] = (Action<Vector>)(_ => { })
            };

            iocMock
                .Setup(i => i.Resolve("Adapters.IMovingObject", It.IsAny<object>()))
                .Returns(adapter);

            var command = new RegisterIoCDependencyMoveCommand(iocMock.Object);

            command.Execute();

            iocMock.Verify(i => i.Register<Func<object, ICommand>>(
                "Commands.Move",
                It.IsAny<Func<object, ICommand>>()
            ), Times.Once);

            Func<object, ICommand> capturedFactory = null;
            iocMock.Verify(i => i.Register(
                "Commands.Move",
                It.Is<Func<object, ICommand>>(f => { capturedFactory = f; return true; })
            ), Times.Once);

            Assert.NotNull(capturedFactory);
            
            var gameObject = new object();
            var result = capturedFactory(gameObject);
            
            Assert.IsType<MoveCommand>(result);
            
            iocMock.Verify(i => i.Resolve("Adapters.IMovingObject", gameObject), Times.Once);
        }
    }
}