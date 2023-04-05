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
using Serilog;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using System.Security.Claims;
using Serilog.Context;
using EticaretAPI.API.Configurations.ColumWriters;
using Microsoft.AspNetCore.HttpLogging;
using EticaretAPI.API.Extensions;
using EticaretAPI.SignalR;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
//    policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()
//));

builder.Services.AddPersistanceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddAplicationServices();
builder.Services.AddSignalRServices();

builder.Services.AddStorage<AzureStorage>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
})
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;

});
Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("log/log.txt")
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"), "logs", needAutoCreateTable: true,
    columnOptions: new Dictionary<string, ColumnWriterBase>
    {
        {"message",new RenderedMessageColumnWriter() },
        {"message_template",new MessageTemplateColumnWriter() },
        {"level",new LevelColumnWriter() },
        {"time_span",new TimestampColumnWriter() },
        {"exception",new ExceptionColumnWriter() },
        {"log_event",new LogEventSerializedColumnWriter() },
        {"user_name",new UsernameColumnWriter() }
    })
    .WriteTo.Seq(builder.Configuration["Seq:ServerUrl"])
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Host.UseSerilog(log);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true,//Oluşturulacak token değerini kimlerin/hangi orjinlerin/hangi sitelerin kullanıcı belirttiğimiz değer
            ValidateIssuer = true,//Oluşturulacak Tokenin kimin Dağıttığını ifade edeceğimiz alan
            ValidateLifetime = true,//oluşturulacak tokenın süresi
            ValidateIssuerSigningKey = true,//Üretilecek token degerinin uygulamaya ait bir özek key ile doğrulama

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Securitykey"])),
             LifetimeValidator=(notBefore,expires,securityToken,validationParameters)=>expires!=null ?expires>DateTime.UtcNow:false,
             NameClaimType=ClaimTypes.Name
             
        };
    });


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials());

app.UseStaticFiles();
app.UseSerilogRequestLogging();
app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());
app.UseHttpsRedirection();
app.UseHttpLogging();
app.UseAuthentication();
app.UseAuthorization();
app.Use(async(context, next) =>
{
    var user = context.User.Identity?.IsAuthenticated !=null || true ? context.User.Identity.Name : null;
    LogContext.PushProperty("user_name",user);
    await next();
});
app.MapControllers();
app.MapHubs();
app.Run();
