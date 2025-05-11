using CalendarPlannerApi;
using CalendarPlannerApi.Helpers;
using CalendarPlannerApi.Interfaces;
using CalendarPlannerApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using CalendarPlannerApi.Services;

var builder = WebApplication.CreateBuilder(args);

const string AllowAllHeadersPolicy = "AllowAllHeadersPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowAllHeadersPolicy,
        builder =>
        {
            builder.WithOrigins("https://localhost:7135")
                    .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PlannerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PlannerDbConnectionString")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = TokenHelper.Issuer,
                ValidAudience = TokenHelper.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(TokenHelper.Secret)),
                ClockSkew = TimeSpan.Zero
            };

        });

builder.Services.AddAuthorization();

builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IActivityService, ActivityService>();

var app = builder.Build();
app.UseCors(AllowAllHeadersPolicy);
// Configure the HTTP request pipeline.
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
