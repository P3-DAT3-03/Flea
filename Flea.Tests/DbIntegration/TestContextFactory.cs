using System;
using Flea.Models;
using Microsoft.EntityFrameworkCore;

namespace Flea.Tests.DbIntegration
{
	/// <summary>
	/// A context factory for use when testing the entity framework
	/// database integration.
	///
	/// It is expected that a connection string specifying the connections parameters
	/// for postrgreSQL server is supplied through the environmental variable
	/// `EFTEST_CONNSTRING`.
	/// </summary>
	public class TestContextFactory: IDbContextFactory<BingoContext>
	{
		private static readonly string ConnectionString;

		static TestContextFactory()
		{
			var connString = Environment.GetEnvironmentVariable("EFTEST_CONNSTRING");
			ConnectionString = connString ?? throw new Exception(
				"Environmental variable EFTEST_CONNSTRING must be present in order to run ef tests.");
		}

		public BingoContext CreateDbContext()
		{
			var options = new DbContextOptionsBuilder<BingoContext>();
			options.UseNpgsql(ConnectionString);
			return new BingoContext(options.Options);
		}
	}
}