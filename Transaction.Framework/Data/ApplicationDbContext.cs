namespace Transaction.Framework.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System;
    using Transaction.Framework.Data.Entities;
    using Transaction.Framework.Data.EntityConfigurations;
public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            _configuration.GetConnectionString("WebApiDatabase"),
            sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        AccountSummaryEntityConfiguration
            .Configure(modelBuilder.Entity<AccountSummaryEntity>());

        AccountTransactionEntityConfiguration
            .Configure(modelBuilder.Entity<AccountTransactionEntity>());

        base.OnModelCreating(modelBuilder);
    }
}
