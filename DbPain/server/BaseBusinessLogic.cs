using DbPain.db;
using Microsoft.EntityFrameworkCore;

namespace DbPain.server.Base
{
    public abstract class BaseBusinessLogic
    {
        protected readonly ShopDbContext _context;

        protected BaseBusinessLogic(ShopDbContext context)
        {
            _context = context;
        }

        protected bool ExecuteOperation(Action operation)
        {
            try
            {
                operation();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
} 