using BloodCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BloodCore.Tests
{
    public class GuidExtensionsTests
    {
        [Fact]
        void ShouldParse_FromBase64ToGuid()
        {
            Guid guid = GuidExtensions.ParseShort("iyQ1EC5EqUm5JDF5FIf54g");

            Assert.Equal("1035248b-442e-49a9-b924-31791487f9e2", guid.ToString());
        }

        [Fact]
        void ShouldEncode_FromGuidToBase64()
        {
            Guid guid = Guid.Parse("1035248b-442e-49a9-b924-31791487f9e2");

            string base64 = guid.ToShort();

            Assert.Equal("iyQ1EC5EqUm5JDF5FIf54g", base64);
        }

        [Theory]
        [InlineData("SGoib8g3GUW7-mp0gbGt4Q")]
        [InlineData("S2Ss8Xp4NkK4-O0RaaW8vQ")]
        [InlineData("1A58_VdUJ0-eM16Foccxvg")]
        [InlineData("jTz0n2OJ5kO6PVThkpuD_Q")]
        [InlineData("iyQ1EC5EqUm5JDF5FIf54g")]
        void ShouldConvert_FromBase64AndToBase64(string testedBase64)
        {
            var guid = GuidExtensions.ParseShort(testedBase64);

            var guidBase64 = guid.ToShort();

            Assert.DoesNotContain("+", guidBase64);
            Assert.DoesNotContain("/", guidBase64);
            Assert.Equal(testedBase64, guidBase64);
        }
    }
}
