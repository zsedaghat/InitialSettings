using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpaceManagment.Infrastructure;
using SpaceManagment.Model;


namespace SpaceManagment.Data
{
    public class SpaceManagmentDbContext : IdentityDbContext<User, Role, long>
    {
        public SpaceManagmentDbContext(DbContextOptions options)
           : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //modelBuilder.Entity<Client>().HasMany(s=>s.Reservations).WithOne(e=>e.Client).HasForeignKey(e=>e.ClientId).OnDelete(DeleteBehavior.Cascade);

            //    modelBuilder.ToTable("Client")
            //.HasOne<Employee>()
            //.WithOne()
            //.HasForeignKey<Manager>(x => x.Id)
            //.OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>(b =>
            {
                b.ToTable("Users");
                b.Ignore(c => c.Email);
                b.Ignore(c => c.EmailConfirmed);
                b.Ignore(c => c.PhoneNumberConfirmed);
                b.Ignore(c => c.NormalizedEmail);
            });
            modelBuilder.Entity<Role>(b =>
            {
                b.ToTable("Role");
            });
            modelBuilder.Entity<IdentityUserLogin<long>>(b =>
            {
                b.ToTable("UserLogins");
            });
            modelBuilder.Entity<IdentityUserClaim<long>>(b =>
            {
                b.ToTable("UserClaim");
            });
            modelBuilder.Entity<IdentityUserRole<long>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            modelBuilder.Entity<IdentityUserToken<long>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
            modelBuilder.Entity<IdentityRoleClaim<long>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<SpaceManagment.Model.Host>().ToTable("Host");
            modelBuilder.Entity<Supervisor>().ToTable("Supervisor");
         
                
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<SpaceManagment.Model.Host> Hosts { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Space> Spaces { get; set; }
        public DbSet<SpaceActiveInterval> SpaceActiveIntervals { get; set; }
        public DbSet<SpaceSupervisor> SpaceSupervisors { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        //public DbSet<SeriLog> SeriLog { get; set; }
    }
}
