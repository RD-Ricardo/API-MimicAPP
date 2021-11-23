using Microsoft.EntityFrameworkCore;
using MimicApp_Api.Models;

namespace MimicApp_Api.Database
{
    public class MimicContext : DbContext
    {
        public MimicContext(DbContextOptions<MimicContext> options) :  base(options)
        {
            
        }
        public DbSet<Palavra> Palavras { get; set; }
    }
}