using System;

namespace DIContainer.BasicDI.UnitTests.TestClasses
{
    internal class TestInterfaceClassC : ITestClassC
    {
        public TestInterfaceClassC()
        {
        }

        public Guid Id { get; set; }
    }
}