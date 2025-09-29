using Microsoft.EntityFrameworkCore;

namespace FAQs.Models
{
    public class FaqsContext : DbContext
    {
        public FaqsContext(DbContextOptions<FaqsContext> options)
            : base(options)
        {
        }
        public DbSet<FAQ> FAQs { get; set; } = null!;
        public DbSet<Topic> Topics { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = "gen", Name = "General" },
                new Category { CategoryId = "hist", Name = "History" }
            );
            modelBuilder.Entity<Topic>().HasData(
                new Topic { TopicId = "asp", Name = "ASP .NET Core" },
                new Topic { TopicId = "blz", Name = "Blazor" },
                new Topic { TopicId = "ef", Name = "Entity Framework" }
            );
            modelBuilder.Entity<FAQ>().HasData(
                new FAQ
                {
                    FAQId = 1,
                    Question = "What is ASP .NET Core?",
                    Answer = "ASP .NET Core is a cross-platform, high-performance framework for building modern, cloud-based, Internet-connected applications.",
                    TopicId = "asp",
                    CategoryId = "gen"
                },
                new FAQ
                {
                    FAQId = 2,
                    Question = "When was ASP .NET Core released?",
                    Answer = "ASP .NET Core was first released in June 2016.",
                    TopicId = "asp",
                    CategoryId = "hist"
                },
                new FAQ
                {
                    FAQId = 3,
                    Question = "What is Blazor?",
                    Answer = "Blazor is a framework for building interactive client-side web UI with .NET.",
                    TopicId = "blz",
                    CategoryId = "gen"
                },
                new FAQ
                {
                    FAQId = 4,
                    Question = "When was Blazor released?",
                    Answer = "Blazor was first released in May 2020.",
                    TopicId = "blz",
                    CategoryId = "hist"
                },
                new FAQ
                {
                    FAQId = 5,
                    Question = "What is Entity Framework?",
                    Answer = "Entity Framework (EF) is an open source object-relational mapping (ORM) framework for ADO.NET.",
                    TopicId = "ef",
                    CategoryId = "gen"
                },
                new FAQ
                {
                    FAQId = 6,
                    Question = "When was Entity Framework released?",
                    Answer = "Entity Framework was first released in 2008.",
                    TopicId = "ef",
                    CategoryId = "hist"
                }
            );
        }
    }
}
