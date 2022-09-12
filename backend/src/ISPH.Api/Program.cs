using System.Globalization;
using System.Reflection;
using ISPH.Data.Contexts;
using ISPH.Domain.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ISPH.Api.Extensions;
using ISPH.Api.Filters;
using AuthenticationOptions = ISPH.Domain.Configuration.AuthenticationOptions;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers(opts =>
{
    opts.Filters.Add<AutomaticRefreshTokenFilter>();
    opts.Filters.Add<ExceptionHandleFilter>();
}).ConfigureApiBehaviorOptions(opt => {
    opt.InvalidModelStateResponseFactory = context => new BadRequestObjectResult(context.ModelState.Values.First(q => q.Errors.Count > 0).Errors.First(er => !string.IsNullOrEmpty(er.ErrorMessage)).ErrorMessage);
});
builder.Services.Configure<AuthenticationOptions>(builder.Configuration.GetSection("Auth"));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Email"));
builder.Services.AddDbContext<ApplicationContext>(o => o.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddApplicationServices();
builder.Services.AddJwtAuthentication(builder.Configuration["Auth:Secret"]);
builder.Services.AddResourceAuthorization();
builder.Services.AddAutoMapper(Assembly.Load("ISPH.Application"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

CultureInfo.CurrentCulture.ClearCachedData();
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