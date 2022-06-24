using Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

InitializeDB(app);

app.Run();


async void InitializeDB(WebApplication app)
{
    using(var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
    {
        var dataContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
        await dataContext.Database.MigrateAsync();
        await Seed.SeedData(dataContext);

        // Seed the data
        /*if(!dataContext.Activities.Any())
        {
            foreach(var client in Config.GetClients())
            {
                dataContext.Clients.Add(client.ToEntity());
            }

            dataContext.SaveChanges();
        }*/
    }
}
