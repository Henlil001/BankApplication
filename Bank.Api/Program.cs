using Bank.Core.Interfaces;
using Bank.Core.Service;
using Bank.Data.DataModels;
using Bank.Data.Interfaces;
using Bank.Data.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authorization with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
         new List<string>()
        }
    });
});


//I f�rsta delen s�tts authentication upp f�r att hanteras med JWT
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
//I andra delen s�tts konfigurationen ac JWT upp
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,              //Validerar att v�r token �r uppsatt av denna app
        ValidateAudience = true,            //Validerar att det �r en verifierad anv�ndare
        ValidateLifetime = true,            //Validerar att aktuell token fortfarande g�ller
        ValidateIssuerSigningKey = true,
        ValidIssuer = "http://localhost:5142",      //Validerar URL d�r token har satts upp
        ValidAudience = "http://localhost:5142",    //Validerar URl som api ligger p�
        IssuerSigningKey =
    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysecretKey12345!#kjbgfoilkjgtiyduglih7gtl8gt5"))     //H�r s�tts krypteringen upp med en nyckel som egentligen inte skall ligga direkt h�r ("mysecretKey12345!#")
    };
});
//Automapper beh�vs s�ttas upp som en service f�r att kunna injecterras n�r man beh�ver anv�nda det
builder.Services.AddAutoMapper(typeof(Program).Assembly);


builder.Services.AddScoped<ILoansService, LoansService>();
builder.Services.AddScoped<ILoansRepo, LoansRepo>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ILoginRepo, LoginRepo>();
builder.Services.AddScoped<IAccountsService, AccountsService>();
builder.Services.AddScoped<IAccountsRepo, AccountsRepo>();
builder.Services.AddScoped<ITransactionsService, TransactionsService>();
builder.Services.AddScoped<ITransactionsRepo, TransactionsRepo>();
builder.Services.AddScoped<IDispositionsRepo, DispositionsRepo>();


builder.Services.AddSingleton<IBankDBContext, BankDBContext>();


var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.UseSwagger();

//Detta �r det verktyg som presenterar dokumentationen i webbl�saren
app.UseSwaggerUI();


app.Run();

