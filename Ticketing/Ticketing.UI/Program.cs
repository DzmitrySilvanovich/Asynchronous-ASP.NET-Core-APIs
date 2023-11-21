using Microsoft.EntityFrameworkCore;
using System;
using Ticketing.BAL.Contracts;
using Ticketing.BAL.Services;
using Ticketing.DAL.Contracts;
using Ticketing.DAL.Domain;
using Ticketing.DAL.Domains;
using Ticketing.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);


string? connection = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddScoped<IVenueService, VenueService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IRepository<Venue>, VenueRepository>();
builder.Services.AddScoped<IRepository<Section>, SectionRepository>();
builder.Services.AddScoped<IRepository<Cart>, CartRepository>();

builder.Services.AddScoped<IRepository<ShoppingCart>, ShoppingCartRepository>();
builder.Services.AddScoped<IRepository<SeatStatus>, SeatStatusRepository>();
builder.Services.AddScoped<IRepository<Seat>, SeatRepository>();
builder.Services.AddScoped<IRepository<PriceType>, PriceTypeRepository>();
builder.Services.AddScoped<IRepository<PaymentStatus>, PaymentStatusRepository>();
builder.Services.AddScoped<IRepository<Payment>, PaymentRepository>();
builder.Services.AddScoped<IRepository<Order>, OrderRepository>();
builder.Services.AddScoped<IRepository<Event>, EventRepository>();

var app = builder.Build();

app.MapControllers();
app.Run();
