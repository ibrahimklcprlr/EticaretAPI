using EticaretAPI.Aplication.Validators.Products;
using EticaretAPI.Persistence;
using FluentValidation.AspNetCore;
using EticaretAPI.Infrastructure.FluentValidation;
using EticaretAPI.Infrastructure;
using EticaretAPI.Infrastructure.Services.Storage;
using EticaretAPI.Infrastructure.Services.Storage.Azure;
using EticaretAPI.Aplication;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
//    policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()
//));
builder.Services.AddPersistanceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddAplicationServices();
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

app.Run();
