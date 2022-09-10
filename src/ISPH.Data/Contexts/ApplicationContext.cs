using ISPH.Domain.Models;
using ISPH.Domain.Models.Advertisements;
using ISPH.Domain.Models.Messaging;
using ISPH.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace ISPH.Data.Contexts;
public sealed class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Employer> Employers { get; set; } = null!;
    public DbSet<Resume> Resumes { get; set; } = null!; 
    public DbSet<Company> Companies { get; set; } = null!;
    public DbSet<Advertisement> Advertisements { get; set; } = null!;
    public DbSet<FeaturedAdvertisement> FeaturedAdvertisements { get; set; } = null!;
    public DbSet<Position> Positions { get; set; } = null!;
    public DbSet<AdvertisementResponse> AdvertisementResponses { get; set; } = null!;
    public DbSet<Chat> Chats { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;
}