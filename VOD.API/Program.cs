using Microsoft.EntityFrameworkCore;
using VOD.Database.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(policy => {
policy.AddPolicy("CorsAllAccessPolicy", opt =>
opt.AllowAnyOrigin()
 .AllowAnyHeader()
 .AllowAnyMethod()
 );
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<VODContext>(
options => options.UseSqlServer(
 builder.Configuration.GetConnectionString("VODConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsAllAccessPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
