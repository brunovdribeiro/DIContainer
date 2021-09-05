namespace DIContainer.BasicDI.UnitTests.TestClasses
{
    internal class TestClassA : ITestClassA
    {
        public TestClassA(TestClassB testClassB, TestClassC testClassC)
        {
        }
    }
}