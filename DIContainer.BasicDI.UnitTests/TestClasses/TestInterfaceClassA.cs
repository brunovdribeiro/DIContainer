namespace DIContainer.BasicDI.UnitTests.TestClasses
{
    internal class TestInterfaceClassA : ITestClassA
    {
        public TestInterfaceClassA(ITestClassB testClassB, ITestClassC testClassC)
        {
        }
    }
}