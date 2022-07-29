using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Validators;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Authentication;
using DataAccessLayer.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProductsApi.Utility;
using IdentityDbContext = DataAccessLayer.DbContexts.IdentityDbContext;
using DataAccessLayer.DbContexts;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var repo = configuration["SourceDb:Connection"];

builder.Services.AddDbContext<EntityDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("EntityConnection")));
builder.Services.AddHttpClient();

if (repo == "Entity")
{
    builder.Services.AddTransient(typeof(IRepository<>), typeof(EntityRepository<>));
}
else if (repo == "Mongo")
{
    builder.Services.AddTransient(typeof(IRepository<>), typeof(MongoRepository<>));
}


builder.Services.AddTransient<IdentityDbContext<User>, IdentityDbContext>();
builder.Services.AddTransient<DbContext, EntityDbContext>();
builder.Services.AddTransient<IValidator<Product>, ProductValidator>();
builder.Services.AddTransient<IValidator<ProductType>, ProductTypeValidator>();
builder.Services.AddTransient<IValidator<Brand>, BrandValidator>();
builder.Services.AddTransient<IValidator<City>, CityValidator>();
builder.Services.AddTransient(typeof(IDataProvider<>), typeof(DataProvider<>));

builder.Services.AddDbContext<IdentityDbContext>(options => 
    options.UseSqlServer(configuration.GetConnectionString("AuthConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,

        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "LeverX Sukhogo Practice",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();