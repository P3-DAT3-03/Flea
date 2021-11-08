namespace Flea.Models
{
    public class Customer
    {
    
        /* TODO add method for creating reservations that link to the customer object*/
        public string Name { get; set; }
        public int PhoneNumber { get; set; }

        public Customer(string name, int phoneNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }
}