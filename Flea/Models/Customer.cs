using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Flea.Models
{
	public class Customer : IModelEntity<Customer, BingoContext>
	{
		// ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
		public int Id { get; set; }

		[StringLength(64)]
		public string Name { get; set; }
		
		[StringLength(8)]
		[Column(TypeName = "VARCHAR")]
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }

		public Customer() : this(0) {}
		
		public Customer(int id) : this(string.Empty, string.Empty)
		{
			Id = id;
		}
		
		public Customer(string name, string phoneNumber)
		{
			Name = name;
			PhoneNumber = phoneNumber;
		}
		
		public Customer Clone() => (Customer) MemberwiseClone();
		
		DbSet<Customer> IModelEntity<Customer, BingoContext>.GetDbSet(BingoContext ctx) => ctx.Customers!;
	}
}