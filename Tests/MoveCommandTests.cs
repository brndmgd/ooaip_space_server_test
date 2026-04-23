using System;
using Xunit;
using OoaipSpaceServer2026.Commands;
using OoaipSpaceServer2026.Models;

namespace Tests
{
    public class MoveCommandTests
    {
        [Fact]
        public void Execute_ObjectAt12_5_WithVelocity_4_1_MovesTo8_6()
        {
            // Arrange
            Vector? currentPosition = new Vector(12, 5);
            var velocity = new Vector(-4, 1);
            Vector? newPosition = null;

            var command = new MoveCommand(
                getPosition: () => currentPosition ?? throw new InvalidOperationException(),
                getVelocity: () => velocity,
                setPosition: v => newPosition = v
            );

            command.Execute();

            Assert.Equal(new Vector(8, 6), newPosition);
        }

        [Fact]
        public void Execute_GetPositionThrows_ThrowsException()
        {
            var command = new MoveCommand(
                getPosition: () => throw new InvalidOperationException("Position unavailable"),
                getVelocity: () => new Vector(1, 2),
                setPosition: _ => { }
            );

            Assert.Throws<InvalidOperationException>(() => command.Execute());
        }

        [Fact]
        public void Execute_GetVelocityThrows_ThrowsException()
        {
            var command = new MoveCommand(
                getPosition: () => new Vector(1, 2),
                getVelocity: () => throw new InvalidOperationException("Velocity unavailable"),
                setPosition: _ => { }
            );

            Assert.Throws<InvalidOperationException>(() => command.Execute());
        }

        [Fact]
        public void Execute_SetPositionThrows_ThrowsException()
        {
            var command = new MoveCommand(
                getPosition: () => new Vector(1, 2),
                getVelocity: () => new Vector(3, 4),
                setPosition: _ => throw new InvalidOperationException("Cannot set position")
            );

            Assert.Throws<InvalidOperationException>(() => command.Execute());
        }
    }
}