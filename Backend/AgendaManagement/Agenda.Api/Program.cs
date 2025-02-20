using Microsoft.EntityFrameworkCore;
using Agenda.Infrastructure.Database;
using Agenda.Infrastructure.Ioc.DependencyInjection;
using Agenda.Api.Endpoints;
using Agenda.Infrastructure.Database.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AgendaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddApplicationServices().AddInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapUserEndpoints();
app.MapAgendaEndpoints();
app.MapEventEndpoints();

app.Run();