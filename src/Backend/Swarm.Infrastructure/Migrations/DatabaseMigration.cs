using Dapper;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace Swarm.Infrastructure.Migrations;

public static class DatabaseMigration
{
    public static void Migrate(string connetionString, IServiceProvider serviceProvider)
    {
        EnsureDatabaseCreated_SqlServer(connetionString);
        MigrationDatabase(serviceProvider);
    }

    private static void EnsureDatabaseCreated_SqlServer(string connetionString)
    {
        var connectionStringBuilder = new SqlConnectionStringBuilder(connetionString);

        var databaseName = connectionStringBuilder.InitialCatalog;

        connectionStringBuilder.Remove("Database");

        using var dbConnection = new SqlConnection(connectionStringBuilder.ConnectionString);

        var parameters = new DynamicParameters();
        parameters.Add("name", databaseName);

        var records = dbConnection.Query("SELECT * FROM sys.databases WHERE name = @name", parameters);

        if (records.Any() == false)
            dbConnection.Execute($"CREATE DATABASE {databaseName}");
    }

    private static void MigrationDatabase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        runner.ListMigrations();
        runner.MigrateUp();
    }
}
