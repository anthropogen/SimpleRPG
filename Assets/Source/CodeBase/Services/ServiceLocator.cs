namespace EpicRPG.Services
{
    public class ServiceLocator
    {
        private static ServiceLocator instance;
        public static ServiceLocator Container => instance ?? (instance = new ServiceLocator());

        public void RegisterSingle<TService>(TService service) where TService : IService
            => ServiceImplementation<TService>.Implementation = service;

        public TService Single<TService>() where TService : class, IService
            => ServiceImplementation<TService>.Implementation;

        private static class ServiceImplementation<TService> where TService : IService
        {
            public static TService Implementation;
        }
    }
}
