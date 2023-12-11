using ExpenseTracker.Core.Configuration;
using ExpenseTracker.Shared.Infrastructure.Settings;
using Microsoft.VisualBasic;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AppSettings.DBProvider = DataBaseProvider.SQLite;
string ConnectionStringSQLServer = "Server=.;Database=expenseTrackerDB_api;User Id=sa;Password=123bh";
string ConnectionStringMySQL = "Server=localhost;Database=expensetrackerdb_api;Uid=root;Pwd=;";
string ConnectionStringSqlLite = "Data Source=expensetrackerdb_api; Version=3";
string connectionType()
{
    string connStr = ConnectionStringSqlLite;
    switch (AppSettings.DBProvider)
    {
    case DataBaseProvider.SqlServer: connStr = ConnectionStringSQLServer;
            break;
    case DataBaseProvider.MySQL: connStr = ConnectionStringMySQL; break;
     default: connStr = ConnectionStringSqlLite; break;
    }
    return connStr;
}
AppSettings.DataBaseSettings = new DataBaseSettings
{
    //ConnectionString = "Server=.;Database=expenseTrackerDB_api;User Id=sa;Password=123bh"// Configuration.GetConnectionString("DBConnection"),//DefaultConnection 
    //ConnectionString = AppSettings.DBProvider == DataBaseProvider.SqlServer ? ConnectionStringSQLServer : ConnectionStringMySQL
    ConnectionString = connectionType()
};

builder.Services.ConfigureUseCase();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
