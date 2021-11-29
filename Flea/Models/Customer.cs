using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Flea.Models
{
	public class Customer : IModelEntity<Customer, BingoContext>
	{
		public int Id { get; set; }

		[StringLength(64, ErrorMessage = "Navnet er for langt.")]
		[Required(ErrorMessage = "Lopper skal have et navn.")]
		public string Name { get; set; }
		[StringLength(64)]
		public string Nickname { get; set; }
		
		[StringLength(8, ErrorMessage = "Telefonnummeret er for langt.")]
		[MinLength(8, ErrorMessage = "Telefonnummeret er for kort.")]
		[Required(ErrorMessage = "Lopper skal have et telefonnummer.")]
		[Column(TypeName = "VARCHAR")]
		[RegularExpression(@"^[0-9]+$", ErrorMessage = "Der er kun tal i telefonnumre.")]
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
			Nickname = "";
			PhoneNumber = phoneNumber;
		}

		public Customer Clone() => (Customer) MemberwiseClone();
		
		DbSet<Customer> IModelEntity<Customer, BingoContext>.GetDbSet(BingoContext ctx) => ctx.Customers!;
	}
	
	public class ValidCustomer : ValidationAttribute
	{

		protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
		{
			if (value is not Customer customer)
				return new ValidationResult(ErrorMessage);
			return customer.Id switch
			{
				<= 0 => new ValidationResult(ErrorMessage),
				_ => ValidationResult.Success!
			};
		}  
	}

}