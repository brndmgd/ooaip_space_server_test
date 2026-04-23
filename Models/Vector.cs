using System;
using System.Collections.Generic;

namespace OoaipSpaceServer2026.Models
{
    public class Vector : IEquatable<Vector>
    {
        private readonly IReadOnlyList<int> _coordinates;

        public IReadOnlyList<int> Coordinates => _coordinates;
        public int Dimension => _coordinates.Count;

        public Vector(params int[] coordinates)
        {
            if (coordinates == null)
                throw new ArgumentNullException(nameof(coordinates));
            if (coordinates.Length == 0)
                throw new ArgumentException("Vector must have at least one coordinate.", nameof(coordinates));

            _coordinates = new List<int>(coordinates).AsReadOnly();
        }

        public Vector Add(Vector other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));
            if (Dimension != other.Dimension)
                throw new ArgumentException($"Cannot add vectors of different dimensions: {Dimension} != {other.Dimension}.");

            var result = new int[Dimension];
            for (int i = 0; i < Dimension; i++)
            {
                result[i] = _coordinates[i] + other._coordinates[i];
            }
            return new Vector(result);
        }

        public static Vector operator +(Vector left, Vector right)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));
            return left.Add(right);
        }

        public override bool Equals(object? obj) => Equals(obj as Vector);

        public bool Equals(Vector? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (Dimension != other.Dimension) return false;

            for (int i = 0; i < Dimension; i++)
            {
                if (_coordinates[i] != other._coordinates[i])
                    return false;
            }
            return true;
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

        public override string ToString() => $"Vector({string.Join(", ", _coordinates)})";
    }
}