namespace DependecyInjectionNamedExtensions.Example.Services
{
    public enum ServiceTypeDescription
    {
        DataServiceA,
        DataServiceB
    }

    public interface IDataService
    {
        string Get();
    }

    public class DataServiceA : IDataService
    {
        public string Get()
        {
            return $"{nameof(DataServiceA)}";
        }
    }

    public class DataServiceB : IDataService
    {
        public string Get()
        {
            return $"{nameof(DataServiceB)}";
        }
    }
}
