using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary
{
    public class Person
    {
        public String Email { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public int ZipCode { get; set; }

        public Person() { }

        public Person(String Email, String FirstName, String LastName, String Address, String City, String State, int ZipCode)
        {
            this.Email = Email;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Address = Address;
            this.City = City;
            this.State = State;
            this.ZipCode = ZipCode;
        }
        
        /*
        public override string ToString()
        {
            return String.Format("Email:{0, -6} | FirstName:{1,-6} | Lastname:{2,6} | Address:{3,-6} | City:{4,-6} | State:{5,-2} | ZipCode:{6,-6}", Email, FirstName, LastName, Address, City, State, ZipCode);
        }
        */
        
    }
}
