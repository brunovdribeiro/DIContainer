using System;
using DIContainer.BasicDI.Exceptions;
using DIContainer.BasicDI.UnitTests.TestClasses;
using Xunit;

namespace DIContainer.BasicDI.UnitTests
{
    public class GetInstanceTests
    {
        private readonly Container _container;

        public GetInstanceTests()
        {
            _container = new Container();
        }

        [Fact]
        public void Should_instantiate_concrete_type_with_no_dependencies()
        {
            _container.RegisterTransient<TestClassC>();

            var instanceObject = _container.GetInstance<TestClassC>();

            Assert.IsType<TestClassC>(instanceObject);
        }

        [Fact]
        public void Should_instantiate_interface_type_with_no_dependencies()
        {
            _container.RegisterTransient<ITestClassC, TestClassC>();

            var instanceObject = _container.GetInstance<ITestClassC>();

            Assert.IsType<TestClassC>(instanceObject);
        }

        [Fact]
        public void Should_instantiate_concrete_type_with_concrete_dependency()
        {
            _container.RegisterTransient<TestClassC>();
            _container.RegisterTransient<TestClassB>();

            var instanceObject = _container.GetInstance<TestClassB>();

            Assert.IsType<TestClassB>(instanceObject);
        }

        [Fact]
        public void Should_instantiate_interface_type_with_concrete_dependency()
        {
            _container.RegisterTransient<TestClassC>();
            _container.RegisterTransient<ITestClassB, TestClassB>();

            var instanceObject = _container.GetInstance<ITestClassB>();

            Assert.IsType<TestClassB>(instanceObject);
        }

        [Fact]
        public void Should_instantiate_interface_type_with_interface_dependency()
        {
            _container.RegisterTransient<ITestClassC, TestInterfaceClassC>();
            _container.RegisterTransient<ITestClassB, TestInterfaceClassB>();

            var instanceObject = _container.GetInstance<ITestClassB>();

            Assert.IsType<TestInterfaceClassB>(instanceObject);
        }

        [Fact]
        public void Should_instantiate_concrete_type_with_multiple_concrete_dependencies()
        {
            _container.RegisterTransient<TestClassA>();
            _container.RegisterTransient<TestClassB>();
            _container.RegisterTransient<TestClassC>();

            var instanceObject = _container.GetInstance<TestClassA>();

            Assert.IsType<TestClassA>(instanceObject);
        }

        [Fact]
        public void Should_instantiate_interface_type_with_multiple_concrete_dependencies()
        {
            _container.RegisterTransient<ITestClassA, TestClassA>();
            _container.RegisterTransient<TestClassB>();
            _container.RegisterTransient<TestClassC>();

            var instanceObject = _container.GetInstance<ITestClassA>();

            Assert.IsType<TestClassA>(instanceObject);
        }

        [Fact]
        public void Should_instantiate_interface_type_with_multiple_interfaces_dependencies()
        {
            _container.RegisterTransient<ITestClassA, TestInterfaceClassA>();
            _container.RegisterTransient<ITestClassB, TestInterfaceClassB>();
            _container.RegisterTransient<ITestClassC, TestInterfaceClassC>();

            var instanceObject = _container.GetInstance<ITestClassA>();

            Assert.IsType<TestInterfaceClassA>(instanceObject);
        }

        [Fact]
        public void Should_instantiate_transient()
        {
            _container.RegisterTransient<ITestClassC, TestClassC>();

            var instanceObject = _container.GetInstance<ITestClassC>();
            var instanceAnotherObject = _container.GetInstance<ITestClassC>();

            Assert.IsType<TestClassC>(instanceObject);

            Assert.NotEqual(instanceObject.Id.ToString(), instanceAnotherObject.Id.ToString());
            Assert.NotEqual(instanceObject.Id.ToString(), Guid.Empty.ToString());
            Assert.NotEqual(instanceAnotherObject.Id.ToString(), Guid.Empty.ToString());
        }

        [Fact]
        public void Should_instantiate_singleton()
        {
            _container.RegisterSingleton<ITestClassC, TestClassC>();

            var instanceObject = _container.GetInstance<ITestClassC>();
            var instanceAnotherObject = _container.GetInstance<ITestClassC>();

            Assert.IsType<TestClassC>(instanceObject);

            Assert.Equal(instanceObject.Id.ToString(), instanceAnotherObject.Id.ToString());
            Assert.NotEqual(instanceObject.Id.ToString(), Guid.Empty.ToString());
            Assert.NotEqual(instanceAnotherObject.Id.ToString(), Guid.Empty.ToString());
        }


        [Fact]
        public void Should_throw_UnregisteredDependencyException_when_instantiating_unregistered_type()
        {
            Assert.Throws<UnregisteredDependencyException>(() => _container.GetInstance<TestClassA>());
        }

        [Fact]
        public void Should_throw_UnregisteredDependencyException_when_instantiating_type_with_unregistered_dependency()
        {
            _container.RegisterTransient<TestClassB>();

            Assert.Throws<UnregisteredDependencyException>(() => _container.GetInstance<TestClassB>());
        }

        [Fact]
        public void Should_throw_CircularDependencyException_when_instantiating_type_with_circular_dependency()
        {
            _container.RegisterTransient<CircularClassA>();
            _container.RegisterTransient<CircularClassB>();
            _container.RegisterTransient<CircularClassC>();

            Assert.Throws<CircularDependencyException>(() => _container.GetInstance<CircularClassA>());
        }
    }
}