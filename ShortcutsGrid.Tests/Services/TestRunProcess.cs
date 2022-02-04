namespace ShortcutsGrid.Tests.Services
{
    using NUnit.Framework;
    using ShortcutsGrid.Services.Run;

    [TestFixture]
    internal class TestRunProcess
    {

        [Test]
        public void TestRunEmpty()
        {
            string result = RunProcess.Run("");
            
            Assert.IsEmpty(result);
        }

    }
}
