using Microsoft.EntityFrameworkCore;
using ENERGY_NOW_BE.Infrastructure;
using Microsoft.AspNetCore.Identity;
using ENERGY_NOW_BE.Application.Auth;
using ENERGY_NOW_BE.Core.Entity;
using ENERGY_NOW_BE.Core.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services for authentication
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<UserRepository>(); 
builder.Services.AddScoped<ClientConfigurationRepository>();
// Configure Entity Framework and MySQL connection
var connectionString = builder.Configuration.GetConnectionString("DevConnection");

builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Configure Identity with roles and users
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Configure Role-based Authorization (optional)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminAccess", policy => policy.RequireRole("ADMIN"));
    options.AddPolicy("ClientAccess", policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole("CLIENT") || context.User.IsInRole("ADMIN")));
    options.AddPolicy("UserAccess", policy => policy.RequireAssertion(context =>
            context.User.IsInRole("USER") || context.User.IsInRole("CLIENT") || context.User.IsInRole("ADMIN")));
});



// Configure Identity options
builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
});

//builder.Services.AddIdentityApiEndpoints<User>()
//    .AddEntityFrameworkStores<DataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable HTTPS Redirection, Authentication, and Authorization
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Map Controllers
app.MapControllers();

app.Run();
