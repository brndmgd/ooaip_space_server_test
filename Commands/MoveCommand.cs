using System;
using OoaipSpaceServer2026.Interfaces;
using OoaipSpaceServer2026.Models;

namespace OoaipSpaceServer2026.Commands
{
    public class MoveCommand : ICommand
    {
        private readonly Func<Vector> _getPosition;
        private readonly Func<Vector> _getVelocity;
        private readonly Action<Vector> _setPosition;

        public MoveCommand(
            Func<Vector> getPosition,
            Func<Vector> getVelocity,
            Action<Vector> setPosition)
        {
            _getPosition = getPosition ?? throw new ArgumentNullException(nameof(getPosition));
            _getVelocity = getVelocity ?? throw new ArgumentNullException(nameof(getVelocity));
            _setPosition = setPosition ?? throw new ArgumentNullException(nameof(setPosition));
        }

        public void Execute()
        {
            var position = _getPosition();
            if (position is null)
                throw new InvalidOperationException("Cannot determine object position.");

            var velocity = _getVelocity();
            if (velocity is null)
                throw new InvalidOperationException("Cannot determine object velocity.");

            var newPosition = position + velocity;
            _setPosition(newPosition);
        }
    }
}