using System;

namespace DIContainer.BasicDI.UnitTests.TestClasses
{
    internal class TestClassC : ITestClassC
    {
        public TestClassC()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}