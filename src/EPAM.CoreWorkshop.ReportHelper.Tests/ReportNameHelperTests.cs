using System.Runtime.InteropServices;
using System.Threading;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EPAM.CoreWorkshop.ReportHelper.Tests
{
    [TestClass]
    public class ReportNameHelperTests
    {
        [TestMethod]
        [DynamicData("NormalizeFileNameTestData", DynamicDataSourceType.Property)]
        public void NormalizeFileNameTest(string name, char repl, string expected)
        {
            var result = ReportNameHelper.NormalizeFileName(name, repl);

            result.Should().BeEquivalentTo(expected);
        }

        [AssemblyInitialize]
        public static void Init(TestContext context)
        {
            Thread.Sleep(30 * 1000);
        }

        static object[][] NormalizeFileNameTestData
        {
            get
            {
                object[][] data = new object[][]
                {
                    new object[]{"Hello: my world", '_', "Hello_ my world"},
                    new object[]{"Hello my %world", '_', "Hello my %world"},
                    new object[]{"Hello my /world", '_', "Hello my _world"}
                };

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    data[0][2] = "Hello: my world";
                    data[1][2] = "Hello my %world";
                    data[2][2] = "Hello my _world";
                }
                return data;
            }
        }
    }
}
