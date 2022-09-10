using System.Reflection;
using System.Text;
using ISPH.Application.Utils.Auth;
using ISPH.Application.Utils.Users;
using ISPH.Data.Contexts;
using ISPH.Domain.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ISPH.Api.Extensions;
using ISPH.Api.Filters;
using AuthenticationOptions = ISPH.Domain.Configuration.AuthenticationOptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opts =>
{
    opts.Filters.Add<ExceptionHandleFilter>();
}).ConfigureApiBehaviorOptions(opt => {
    opt.InvalidModelStateResponseFactory = context => 
        new BadRequestObjectResult(context.ModelState.Values.First(q => q.Errors.Count > 0).Errors
            .First(er => !string.IsNullOrEmpty(er.ErrorMessage)).ErrorMessage);
});
builder.Services.Configure<AuthenticationOptions>(builder.Configuration.GetSection("Auth"));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Email"));
builder.Services.AddDbContext<ApplicationContext>(o => o.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
//builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));
builder.Services.AddApplicationServices();
builder.Services.AddAutoMapper(Assembly.Load("ISPH.Application"));
builder.Services.AddSingleton<JwtService>();
builder.Services.AddSingleton<EmailNotifier>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Auth:Secret"])),
            ValidateIssuerSigningKey = true,
        };
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(b => b.SetIsOriginAllowed(_ => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();