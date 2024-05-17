using System.Reflection;
using FinanceTracker.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.DAL.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options) {}
    
    public DbSet<UserModel> UserData { get; set; }
    public DbSet<StatusModel> UserStatus { get; set; }
    public DbSet<RoleModel> Role { get; set; }
    public DbSet<GenderModel> Gender { get; set; }
    public DbSet<CountryModel> Countries { get; set; }
    public DbSet<StateModel> States { get; set; }
    public DbSet<CityModel> Cities { get; set; }
    public DbSet<CategoryTypeModel> CategoryTypes { get; set; }
    public DbSet<CategoryModel> Categories { get; set; }
    public DbSet<TransactionModel> Transactions { get; set; }
    public DbSet<NotificationContentModel> NotificationContents { get; set; }
    public DbSet<NotificationModel> Notifications { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<UserModel>()
                    .ToTable("User", "User")
                    .HasKey(x => x.UserId);

        modelBuilder.Entity<StatusModel>()
                    .ToTable("UserStatus", "Ref")
                    .HasKey(x => x.UserStatusId);

        modelBuilder.Entity<RoleModel>()
                    .ToTable("Role", "Ref")
                    .HasKey(x => x.RoleId);

        modelBuilder.Entity<GenderModel>()
                    .ToTable("Gender", "Ref")
                    .HasKey(x => x.GenderId);

        modelBuilder.Entity<CountryModel>()
                    .ToTable("Country", "Ref")
                    .HasKey(x => x.CountryId);

        modelBuilder.Entity<StateModel>()
                    .ToTable("State", "Ref")
                    .HasKey(x => x.StateId);

        modelBuilder.Entity<CityModel>()
                    .ToTable("City", "Ref")
                    .HasKey(x => x.CityId);

        modelBuilder.Entity<CategoryTypeModel>()
                    .ToTable("CategoryType", "Ref")
                    .HasKey(x => x.CategoryTypeId);

        modelBuilder.Entity<CategoryModel>()
                    .ToTable("Category", "Ref")
                    .HasKey(x => x.CategoryId);

        modelBuilder.Entity<TransactionModel>()
                    .ToTable("Transactions", "Finance")
                    .HasKey(x => x.TransactionId);

        modelBuilder.Entity<NotificationContentModel>()
                    .ToTable("NotificationContent", "Ref")
                    .HasKey(x => x.NotificationContentId);
                    
       modelBuilder.Entity<NotificationModel>()
                    .ToTable("Notification", "User")
                    .HasKey(x => x.NotificationId);
    }
}
