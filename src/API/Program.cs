using Microsoft.EntityFrameworkCore;
using Persistence;
using MediatR;
using Application.Products.Commands;
using Application.Interfaces;
using Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);


//services
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddMediatR(typeof(CreateProduct.Handler).Assembly);

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddEndpointsApiExplorer();

//build app
var app = builder.Build();

//middleware and endpoints
app.MapControllers();

//run app
app.Run();