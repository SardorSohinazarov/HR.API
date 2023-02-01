using HR.API.Models;
using HR.API.Services;
using HR.DataAccess.Entities;
using HR.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextPool<AppDbContext>(options 
    => options.UseSqlServer(builder.Configuration.GetConnectionString("HR")));
builder.Services.AddScoped<IGenericCRUDService<EmployeeModel>, EmployeeCRUDService>();
builder.Services.AddScoped<IGenericCRUDService<AddressModel>, AddressCRUDService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGenericRepository<Employee>, EmployeeRepository>();
builder.Services.AddScoped<IGenericRepository<Address>, AddressRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
