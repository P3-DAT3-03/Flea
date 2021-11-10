namespace Flea.Models
{
	public class Customer
	{
		public int Id { get; set; }

		public string Name { get; set; }
		public string PhoneNumber { get; set; }

		public Customer(string name, string phoneNumber)
		{
			Name = name;
			PhoneNumber = phoneNumber;
		}

		public Reservation CreateReservation(int priority, int tables, bool paid, string comments)
		{
			return new Reservation(priority, tables, paid, comments, this);
		}
	}
}