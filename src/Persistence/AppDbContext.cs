using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public required DbSet<Product> Products { get; set; }
    public required DbSet<Category> Categories { get; set; }
}
