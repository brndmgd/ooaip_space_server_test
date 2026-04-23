using Xunit;
using OoaipSpaceServer2026.Models;

namespace Tests
{
    public class VectorTests
    {
        [Fact]
        public void Add_VectorsWithOppositeCoordinates_ReturnsZeroVector()
        {
            var v1 = new Vector(1, -1, 2);
            var v2 = new Vector(-1, 1, -2);
            var result = v1 + v2;

            Assert.Equal(new Vector(0, 0, 0), result);
        }

        [Fact]
        public void Add_VectorsWithDifferentDimensions_LeftLonger_ThrowsArgumentException()
        {
            var v1 = new Vector(1, 2, 3);
            var v2 = new Vector(1, 2);

            Assert.Throws<ArgumentException>(() => v1 + v2);
        }

        [Fact]
        public void Add_VectorsWithDifferentDimensions_RightLonger_ThrowsArgumentException()
        {
            var v1 = new Vector(1, 2);
            var v2 = new Vector(1, 2, 3);

            Assert.Throws<ArgumentException>(() => v1 + v2);
        }

        [Fact]
        public void Equals_CoordinatesMatch_DifferentObjects_ReturnsTrue()
        {
            var v1 = new Vector(1, 2, 3);
            var v2 = new Vector(1, 2, 3);

            Assert.True(v1.Equals(v2));
        }

        [Fact]
        public void OperatorEquals_CoordinatesMatch_DifferentObjects_ReturnsTrue()
        {
            var v1 = new Vector(1, 2, 3);
            var v2 = new Vector(1, 2, 3);

            Assert.True(v1 == v2);
        }

        [Fact]
        public void Equals_CoordinatesDiffer_ReturnsFalse()
        {
            var v1 = new Vector(1, 2, 3);
            var v2 = new Vector(1, 2, 4);

            Assert.False(v1.Equals(v2));
        }

        [Fact]
        public void OperatorNotEqual_CoordinatesDiffer_ReturnsTrue()
        {
            var v1 = new Vector(1, 2, 3);
            var v2 = new Vector(1, 2, 4);

            Assert.True(v1 != v2);
        }

        [Fact]
        public void GetHashCode_VectorHasHashCode()
        {
            var v = new Vector(1, 2, 3);
            var hashCode = v.GetHashCode();

            Assert.NotEqual(0, hashCode);
        }
    }
}