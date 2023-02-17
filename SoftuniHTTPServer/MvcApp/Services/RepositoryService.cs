namespace MvcApp.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly IRepositoryService repositoryService;

        public RepositoryService(IRepositoryService repositoryService)
        {
            this.repositoryService = repositoryService;
        }

        public void AddRepository()
        {
            throw new NotImplementedException();
        }
    }
}
