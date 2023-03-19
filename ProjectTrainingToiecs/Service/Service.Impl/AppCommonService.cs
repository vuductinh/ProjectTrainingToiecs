namespace ProjectTrainingToiecs.Service.Service.Impl
{
    public class AppCommonService
    {
        private readonly DbTrainingToiecsContext _context;
        public AppCommonService(DbTrainingToiecsContext context)
        {
            _context = context;
        }
    }
}
