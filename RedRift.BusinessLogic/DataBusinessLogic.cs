using Microsoft.EntityFrameworkCore;

using RedRift.DataAccess;

using System.Threading.Tasks;

namespace RedRift.BusinessLogic
{
    public class DataBusinessLogic : IDataBusinessLogic
    {
        private readonly RedRiftContext _context;

        public DataBusinessLogic(RedRiftContext context)
        {
            _context = context;
        }

        public async Task Migrate()
        {
            await _context.Database.MigrateAsync();
        }
    }
}
