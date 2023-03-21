using EticaretAPI.Aplication.Validators.Products;
using EticaretAPI.Persistence;
using FluentValidation.AspNetCore;
using EticaretAPI.Infrastructure.FluentValidation;
using EticaretAPI.Infrastructure;
using EticaretAPI.Infrastructure.Services.Storage;
using EticaretAPI.Infrastructure.Services.Storage.Azure;
using EticaretAPI.Aplication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
//    policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()
//));
builder.Services.AddPersistanceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddAplicationServices();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("Admin",options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateAudience = true,//Olu�turulacak token de�erini kimlerin/hangi orjinlerin/hangi sitelerin kullan�c� belirtti�imiz de�er
        ValidateIssuer = true,//Olu�turulacak Tokenin kimin Da��tt���n� ifade edece�imiz alan
        ValidateLifetime = true,//olu�turulacak token�n s�resi
        ValidateIssuerSigningKey = true,//�retilecek token degerinin uygulamaya ait bir �zek key ile do�rulama

        ValidAudience=builder.Configuration["Token:Audince"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Securitykey"]))
    };
});
builder.Services.AddStorage<AzureStorage>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
})
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();
app.UseAuthorization();

app.Run();
