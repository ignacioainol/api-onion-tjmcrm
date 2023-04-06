using Application;
using MediatR;
using Persistence;
using Shared;
using TJCMCRM.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


// Add aplication Layer
builder.Services.AddApplicationLayer();
// Add infrastructured shared Layer
builder.Services.AddSharedInfraestructure(builder.Configuration);
// Add persistence Layer
builder.Services.AddPersistenceInfraestructure(builder.Configuration);

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApiVersioningExtension();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("corsapp");
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseErrorHandlingMiddleware();
app.MapControllers();

app.Run();
