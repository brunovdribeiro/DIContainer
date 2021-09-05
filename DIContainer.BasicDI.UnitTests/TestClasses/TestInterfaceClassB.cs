namespace DIContainer.BasicDI.UnitTests.TestClasses
{
    internal class TestInterfaceClassB : ITestClassB
    {
        public TestInterfaceClassB(ITestClassC testClassC)
        {
        }
    }
}