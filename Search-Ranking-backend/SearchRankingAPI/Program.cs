using ScarperSelenium;
using SearchRankingBL;
using SearchRankingDL;
using Serilog;

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog(Log.Logger);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// 
builder.Services.AddTransient<ISearchWorker,SearchWorker>();
builder.Services.AddTransient<IDatalayer, Datalayer>();
builder.Services.AddTransient<IScarper, Scarper>();
builder.Services.AddTransient<ISearchWorker, SearchWorker>();

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
