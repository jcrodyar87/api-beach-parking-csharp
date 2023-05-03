using api_beach_parking_csharp.Models;
using api_beach_parking_csharp.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace api_beach_parking_csharp.Datos
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        
        }
        public DbSet<Client> clients { get; set; }
        public DbSet<Car> cars { get; set; }
        public DbSet<Place> places { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Client>().HasData(
            new Client() {
                id = 1, 
                first_name = "Juan",
                last_name = "Rodriguez",
                email = "jcry87@gmail.com", 
                phone = "999999999",
                status = 1,
                creation_date = DateTime.Now,
                updated_date = DateTime.Now,
            },
            new Client() { 
                id = 2, 
                first_name = "Carlos", 
                last_name = "Rodriguez", 
                email = "jcry1987@gmail.com", 
                phone = "909999999",
                status = 1,
                creation_date = DateTime.Now,
                updated_date = DateTime.Now,
            }
           );
            modelBuilder.Entity<Car>().HasData(
            new Car() 
            { 
                id = 1, 
                code = "REF-717", 
                name = "Wolkswagen Gol Azul",
                model = "Wolkswagen", 
                color = "azul", 
                seats_quantity = 5, 
                status = 1,
                creation_date = DateTime.Now,
                updated_date = DateTime.Now,
            },
            new Car() { 
                id = 2, 
                code = "A1A-123", 
                name = "Nissan 2023", 
                model = "Nissan", 
                color = "rojo", 
                seats_quantity = 5, 
                status = 1,
                creation_date = DateTime.Now,
                updated_date = DateTime.Now,
            }
            );
        }
    }
}
