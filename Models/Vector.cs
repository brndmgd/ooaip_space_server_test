using System;
using System.Collections.Generic;

namespace OoaipSpaceServer2026.Models
{
    public class Vector
    {
        private readonly int[] _coordinates;

        public int[] Coordinates => _coordinates;
        public int Dimension => _coordinates.Length;

        public Vector(params int[] coordinates)
        {
            if (coordinates is null)
                throw new ArgumentNullException(nameof(coordinates));
            if (coordinates.Length == 0)
                throw new ArgumentException("Vector must have at least one coordinate.", nameof(coordinates));

            _coordinates = coordinates;
        }

        public Vector Add(Vector other)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other));
            if (Dimension != other.Dimension)
                throw new ArgumentException($"Cannot add vectors of different dimensions: {Dimension} != {other.Dimension}.");

            int[] result = _coordinates.Zip(other.Coordinates, (x, y) => x + y).ToArray();
            return new Vector(result);
        }

        public static Vector operator +(Vector left, Vector right)
        {
            if (left is null) throw new ArgumentNullException(nameof(left));
            if (right is null) throw new ArgumentNullException(nameof(right));
            return left.Add(right);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }
            if (obj is Vector other)
            {
                return _coordinates.SequenceEqual(other.Coordinates);
            }

            return false;
        }

        public static bool operator ==(Vector left, Vector right)
        {
            if (left is null) return right is null;
            return left.Equals(right);
        }

        public static bool operator !=(Vector left, Vector right) => !(left == right);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                foreach (var coord in _coordinates)
                {
                    hash = hash * 23 + coord.GetHashCode();
                }
                return hash;
            }
        }
    }
}