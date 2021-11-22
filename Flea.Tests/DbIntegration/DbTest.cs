using System.Threading.Tasks;
using NUnit.Framework;

namespace Flea.Tests.DbIntegration
{
	/// <summary>
	/// A base class that all entity framework tests should inherit
	/// from. It implements the basic setup routine. This setup
	/// routine will ensure that all tests are run on a clean
	/// slate using an empty database with the base table initialised.
	/// </summary>
	public abstract class DbTest
	{
		protected readonly TestContextFactory Factory = new();

		[SetUp]
		public async Task Setup()
		{
			await using var ctx = Factory.CreateDbContext();
			await ctx.Database.EnsureDeletedAsync(); // Wipe the database
			await ctx.Database.EnsureCreatedAsync(); // Rebuild tables
		}
	}
}