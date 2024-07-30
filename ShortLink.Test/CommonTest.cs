using ShortLink.Helpers;

namespace ShortLink.Test;
[TestClass]
public class CommonTest
{
    public TestContext TestContext { get; set; }//magic property

    [TestMethod]
    public void CryptoGenTest()
    {
        for (short j = 7; j < 12; ++j)
        {
            for (var i = 0; i < 10; ++i)
            {
                var str = CryptoHelper.GetRandomString(j);
                TestContext.WriteLine($"link [{str}] len [{str.Length}]");
                Assert.AreEqual(j, str.Length);
            }
        }
    }
}