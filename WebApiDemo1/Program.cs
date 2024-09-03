using Microsoft.EntityFrameworkCore;
using WebApiDemo1.Data;
using WebApiDemo1.Formatters;
using WebApiDemo1.Repository.Abstract;
using WebApiDemo1.Repository.Concrete;
using WebApiDemo1.Services.Abstract;
using WebApiDemo1.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.OutputFormatters.Insert(0,new CSVOutputFormatter());
    options.InputFormatters.Insert(0,new VCardInputFormatter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IStudentRepository,StudentRepository>();
builder.Services.AddScoped<IStudentService,StudentService>();

var conn = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<StudentDBContext>(opt =>
{
    opt.UseSqlServer(conn);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
