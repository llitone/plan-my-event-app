using files_service_data.Entities;
using Microsoft.EntityFrameworkCore;

namespace files_service_data;

public class FilesServiceContext : DbContext
{
    public FilesServiceContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Image> Images { get; set; }
}
