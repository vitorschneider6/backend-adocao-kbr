using System.Text;
using Adocao;
using Adocao.Data;
using Adocao.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddDbContext<AdocaoDevDataContext>();
builder.Services.AddTransient<TokenService>();
builder.Services.AddTransient<EmailService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "PoliticaAPI",
        policy =>
        {
            policy.WithOrigins("https://vitor-adocao-kbr.vercel.app/")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

var key = Encoding.ASCII.GetBytes(Configuration.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();
app.MapControllers();

Configuration.Secret = app.Configuration.GetValue<string>("Secret");
Configuration.BlobConnectionString = app.Configuration.GetValue<string>("BlobConnectionString");
Configuration.ConnectionString = app.Configuration.GetConnectionString("DefaultConnection");

var smtp = new Configuration.ConfiguracaoSmtp();
app.Configuration.GetSection("Smtp").Bind(smtp);
Configuration.Smtp = smtp;

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("PoliticaAPI");

app.Run();
