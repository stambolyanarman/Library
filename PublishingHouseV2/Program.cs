using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Interfaces;
using Library.UnitOfWorks;
using Library.DataLayer.Repository;
using Library.DataLayer.Interfaces;
using Library.BusinessLayer.Services;
using Library.BusinessLayer.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LibraryContext>(
    opt =>
    {
        opt.UseNpgsql(builder.Configuration.GetConnectionString("Library"));
    });
builder.Services.AddControllers().AddNewtonsoftJson(
    options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });
builder.Services.AddHttpLogging(o => { });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddScoped<IAuthorsService, AuthorsService>();
builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddStackExchangeRedisCache(option =>
{
    option.Configuration = builder.Configuration["redis"];
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();

app.UseExceptionHandler(o => { });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
