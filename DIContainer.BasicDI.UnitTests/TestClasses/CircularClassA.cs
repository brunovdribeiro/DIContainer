namespace DIContainer.BasicDI.UnitTests.TestClasses
{
    internal class CircularClassA
    {
        public CircularClassA(CircularClassB circularClassB)
        {
        }
    }
}