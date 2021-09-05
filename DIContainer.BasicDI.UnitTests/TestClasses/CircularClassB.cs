namespace DIContainer.BasicDI.UnitTests.TestClasses
{
    internal class CircularClassB
    {
        public CircularClassB(CircularClassC circularClassC)
        {
        }
    }
}