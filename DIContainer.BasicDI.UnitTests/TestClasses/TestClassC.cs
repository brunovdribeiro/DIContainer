using System;

namespace DIContainer.BasicDI.UnitTests.TestClasses
{
    internal class TestClassC : ITestClassC
    {
        public Guid Id { get; set; }

        public TestClassC()
        {
            Id = Guid.NewGuid();
        }
    }
}