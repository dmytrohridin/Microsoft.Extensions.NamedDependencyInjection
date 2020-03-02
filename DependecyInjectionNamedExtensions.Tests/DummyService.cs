namespace DependecyInjectionNamedExtensions.Tests
{
    public interface IDummyService
    {
        string GetServiceName();
    }

    public class DummyService : IDummyService
    {
        public string GetServiceName() => nameof(DummyService);
    }
}
