using HomeFloory.Data;
using HomeFloory.Repositories.AdresaIsporukeRepo;
using HomeFloory.Repositories.DodatiProizvodiRepo;
using HomeFloory.Repositories.DostavaRepo;
using HomeFloory.Repositories.KategorijaRepo;
using HomeFloory.Repositories.KorisnikRepo;
using HomeFloory.Repositories.KorpaRepo;
using HomeFloory.Repositories.Payment;
using HomeFloory.Repositories.PlacanjeRepo;
using HomeFloory.Repositories.ProizvodjacRepo;
using HomeFloory.Repositories.ProizvodRepo;
using HomeFloory.Repositories.UlogaRepo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Autorizacija",
        Description = "Unesite validan JWT bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securityScheme, new string[] { } }
    });
});

builder.Services.AddDbContext<HomeFlooryDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("HomeFloory"));
});

builder.Services.AddScoped<IAdresaIsporukeRepo,AdresaIsporukeRepo>();
builder.Services.AddScoped<IUlogaRepo, UlogaRepo>();
builder.Services.AddScoped<IKategorijaRepo, KategorijaRepo>();
builder.Services.AddScoped<IDostavaRepo, DostavaRepo>();
builder.Services.AddScoped<IProizvodjacRepo, ProizvodjacRepo>();
builder.Services.AddScoped<IProizvodRepo, ProizvodRepo>();
builder.Services.AddScoped<IKorpaRepo, KorpaRepo>();
builder.Services.AddScoped<IDodatiProizvodiRepo, DodatiProizvodiRepo>();
builder.Services.AddScoped<IPlacanjeRepo, PlacanjeRepo>();
builder.Services.AddScoped<IKorisnikRepo, KorisnikRepo>();
builder.Services.AddScoped<IPaymentRepo, PaymentRepo>();

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
