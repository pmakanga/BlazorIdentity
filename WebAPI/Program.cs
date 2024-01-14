using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// added the below to configs to default webapi run webassembly project
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddControllers();// added
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication(); // added (auth)
builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<DataContext>(); //added (auth)

// use swagger to test the token
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging(); // added (wasm)
}

app.MapIdentityApi<IdentityUser>(); // added (map identity api)
app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles(); // added (wasm)
app.UseStaticFiles(); // added (wasm)
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("Index.html"); // added (wasm)
app.Run();
