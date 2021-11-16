using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Flea.Models
{
	public class Customer : IModelEntity<Customer, BingoContext>
	{
		public int Id { get; set; }

		[StringLength(64)]
		public string Name { get; set; }
		
		[StringLength(8)]
		[Column(TypeName = "VARCHAR")]
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }

		public Customer() : this(0) {}
		
		public Customer(int id)
		{
			Id = id;
			Name = string.Empty;
			PhoneNumber = string.Empty;
		}
		
		public Customer(string name, string phoneNumber)
		{
			Name = name;
			PhoneNumber = phoneNumber;
		}

		public Reservation CreateReservation(int priority, int tables, bool paid, string comments)
		{
			return new Reservation(priority, tables, paid, comments, this);
		}
		public Customer Clone() => (Customer) this.MemberwiseClone();
		
		DbSet<Customer> IModelEntity<Customer, BingoContext>.GetDbSet(BingoContext ctx) => ctx.Customers;
	}
}