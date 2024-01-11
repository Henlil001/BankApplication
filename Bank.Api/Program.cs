using Bank.Core.Interfaces;
using Bank.Core.Service;
using Bank.Data.DataModels;
using Bank.Data.Interfaces;
using Bank.Data.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Configuration.AddJsonFile("appsettings.json");

//I första delen sätts authentication upp för att hanteras med JWT
builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
//I andra delen sätts konfigurationen ac JWT upp
.AddJwtBearer(opt => {
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,              //Validerar att vår token är uppsatt av denna app
        ValidateAudience = true,            //Validerar att det är en verifierad användare
        ValidateLifetime = true,            //Validerar att aktuell token fortfarande gäller
        ValidateIssuerSigningKey = true,
        ValidIssuer = "http://localhost:5142",      //Validerar URL där token har satts upp
        ValidAudience = "http://localhost:5142",    //Validerar URl som api ligger på
        IssuerSigningKey =
    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysecretKey12345!#kjbgfoilkjgtiyduglih7gtl8gt5"))     //Här sätts krypteringen upp med en nyckel som egentligen inte skall ligga direkt här ("mysecretKey12345!#")
    };
});



builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddSingleton<IBankDBContext, BankDBContext>();



//Automapper behövs sättas upp som en service för att kunna injecterras när man behöver använda det
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();



app.Run();
