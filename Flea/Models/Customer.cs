namespace Flea.Models
{
    public class Customer
    {
        /*simple customer class has public name and phonenumber. 
         * TODO add method for creating reservations that link to the customer object*/
        public string Name { get; set; }
        public int PhoneNumber { get; set; }

        public Customer(string name, int phoneNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }
}