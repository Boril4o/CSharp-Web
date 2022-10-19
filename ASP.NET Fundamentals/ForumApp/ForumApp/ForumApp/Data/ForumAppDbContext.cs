using Microsoft.EntityFrameworkCore;
using ForumApp.Data.Entities;

namespace ForumApp.Data
{
    public class ForumAppDbContext : DbContext
    {
        private Post FirstPost { get; set; }

        private Post SecondPost { get; set; }

        private Post ThirdPost { get; set; }

        public ForumAppDbContext(DbContextOptions<ForumAppDbContext> options)
            : base(options)
        {
            this.Database.Migrate();
        }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder
            //    .Entity<Post>()
            //    .HasMany(p => p.PostAnswers)
            //    .WithOne(r => r.Post)
            //    .OnDelete(DeleteBehavior.Restrict);

            SeedPosts();
            builder
                .Entity<Post>()
                .HasData(this.FirstPost,
                 this.SecondPost,
                this.ThirdPost);

            base.OnModelCreating(builder);
        }

        private void SeedPosts()
        {
            this.FirstPost = new Post
            {
                Id = 1,
                Title = "My First Post",
                Content = "My first post will be about performing CRUD operations in MVC. Its so much fun!"
            };

            this.SecondPost = new Post
            {
                Id = 2,
                Title = "My Second Post",
                Content = "This is my second post CRUD operations in MVC are getting more and more intresting!"
            };

            this.ThirdPost = new Post
            {
                Id = 3,
                Title = "My Third Post",
                Content = "Hello there! Im getting better and better with the CRUD operations in MVC. Stay tuned!"
            };
        }
    }
}
