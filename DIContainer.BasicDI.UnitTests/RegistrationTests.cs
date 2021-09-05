using DIContainer.BasicDI.Exceptions;
using DIContainer.BasicDI.UnitTests.TestClasses;
using Xunit;

namespace DIContainer.BasicDI.UnitTests
{
    public class RegistrationTests
    {
        private readonly Container _container;

        public RegistrationTests()
        {
            _container = new Container();
        }

        [Fact]
        public void Should_register_concrete_types()
        {
           

            _container.RegisterTransient<TestClassA>();
            _container.RegisterTransient<TestClassB>();
            _container.RegisterTransient<TestClassC>();

            Assert.Equal(3, _container.Registrations());
        }
        
        [Fact]
        public void Should_register_inteface_and_concrete_types()
        {
           

            _container.RegisterTransient<ITestClassA, TestClassA>();
            _container.RegisterTransient<ITestClassB, TestClassB>();
            _container.RegisterTransient<ITestClassC, TestClassC>();

            Assert.Equal(3, _container.Registrations());
        }

        [Fact]
        public void Should_throw_AlreadyRegisteredException_when_registering_dependency_more_than_once()
        {
            _container.RegisterTransient<TestClassA>();

            Assert.Throws<AlreadyRegisteredException>(() => _container.RegisterTransient<TestClassA>() );
        }
        
        [Fact]
        public void Should_throw_AlreadyRegisteredException_when_registering_a_interface_dependency_more_than_once()
        {
            _container.RegisterTransient<ITestClassA, TestClassA>();

            Assert.Throws<AlreadyRegisteredException>(() => _container.RegisterTransient<ITestClassA, TestClassA>() );
        }
        
        [Fact]
        public void Should_throw_AbstractionOrInterfaceException_when_registering_a_interface_instead_of_concrete()
        {
            Assert.Throws<AbstractionOrInterfaceException>(() => _container.RegisterTransient<ITestClassC>() );
        }
    }
}