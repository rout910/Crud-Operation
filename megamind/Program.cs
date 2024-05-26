using megamind.Repositories;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<CrudRepository>();


builder.Services.AddSingleton<NpgsqlConnection>( (ServiceProvider) =>
{
    var Configuration = ServiceProvider.GetRequiredService<IConfiguration>();
    var ConnectionString = Configuration.GetConnectionString("DefaultConnection");
    return new NpgsqlConnection(ConnectionString);
}

);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
