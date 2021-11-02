using Microsoft.EntityFrameworkCore;

namespace Flea.Models
{
    public class BingoContext : DbContext
    {
        public BingoContext(DbContextOptions<BingoContext> contextOptions) : base(contextOptions)
        {
            
        }
    }
}