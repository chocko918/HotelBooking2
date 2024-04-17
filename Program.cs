using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text;
using HotelBooking2.Repositories;
using HotelBooking2.Infrastructure;
using Microsoft.OpenApi.Models;
using HotelBooking2.CustomerValidation;
using FluentValidation;
using HotelBooking2.Models;
using FluentValidation.Internal;


var builder = WebApplication.CreateBuilder(args);

// Add services W the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel Booking", Version = "1.0" });
});



// Add your DbContext configuration here
builder.Services.AddDbContext<HotelBookingDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddTransient<AbstractValidator<CreateCustomerDTO>, CreateCustomerValidator>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();




var app = builder.Build();

// Configure the HTTP request pipeline.
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel Booking");
    });
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.Run();
