using Microsoft.EntityFrameworkCore;

namespace SharpTranslate.Models.DatabaseModels.Core;

public class DataContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    
    public virtual DbSet<UserWordPair> UsersWordPairs { get; set; }

    public virtual DbSet<Word> Words { get; set; }

    public virtual DbSet<WordPair> WordPairs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
}