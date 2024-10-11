using UnifyCov;

namespace UnifyCov.Test
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void TestMethod1()
    {
      int result = Integration.SomaTeste(2, 2);
      Assert.AreEqual(4, result);
    }
  }
}