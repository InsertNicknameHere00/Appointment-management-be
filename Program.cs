using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using AppointmentAPI.Repository;
using AppointmentAPI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole(options =>
{
   // options.IncludeScopes = true;
    options.TimestampFormat = "HH:mm:ss ";
});

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HaircutSalonDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddTransient<IEmailSendService, EmailSendService>();

builder.Services.AddScoped<ISalonServices, SalonServices>();
builder.Services.AddScoped(typeof(ISalonServiceRepository), typeof(SalonServiceRepository));
builder.Services.AddScoped(typeof(IAdminServiceRepository), typeof(AdminServiceRepository));
builder.Services.AddScoped<IAdminServices,AdminServices>();

builder.Services.AddScoped(typeof(IReviewRepository), typeof(ReviewRepository));
builder.Services.AddScoped<IReviewService, ReviewService>();

builder.Services.AddScoped<IUsersServices, UsersServices>();
builder.Services.AddScoped<IUsersServiceRepository, UsersServiceRepository>();
builder.Services.AddScoped(typeof(IUsersServices), (typeof(UsersServices)));
builder.Services.AddScoped(typeof(IUsersServiceRepository), (typeof(UsersServiceRepository)));

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

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapSwagger().RequireAuthorization();
app.MapControllers();

app.Run();
