using ShortLink.Helpers;

namespace ShortLink.Test;
[TestClass]
public class CommonTest
{
    /// <summary>
    /// Gets or sets the test context which provides
    /// information about and functionality for the current test run.
    /// </summary>
    public TestContext TestContext { get; set; }
    //private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    [TestMethod]
    public void CryptoGenTest()
    {
        for (short j = 7; j < 12; ++j)
        {
            for (var i = 0; i < 10; ++i)
            {
                var str = CryptoHelper.GetRandomString(j);
                TestContext.WriteLine(str);
                Assert.AreEqual(j, str.Length);
            }
        }
    }
}