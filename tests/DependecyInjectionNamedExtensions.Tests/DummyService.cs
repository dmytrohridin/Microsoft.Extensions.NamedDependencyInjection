namespace DependecyInjectionNamedExtensions.Tests
{
    public static class DummyServiceConstants
    {
        public const string DummyServiceAKey = nameof(DummyServiceA);
        public const string DummyServiceBKey = nameof(DummyServiceB);
    } 

    public enum DummyServiceType
    {
        DummyServiceA,
        DummyServiceB
    }

    public interface IDummyService
    {
        string GetServiceName();
    }

    public class DummyServiceA : IDummyService
    {
        public string GetServiceName() => nameof(DummyServiceA);
    }

    public class DummyServiceB : IDummyService
    {
        public string GetServiceName() => nameof(DummyServiceB);
    }
}
