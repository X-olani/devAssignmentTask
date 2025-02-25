using System.ComponentModel;

namespace frontend.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [DisplayName("First Name ")]
        public string firstName { get; set; }

        [DisplayName("Last Name ")]
        public string lastName { get; set; }

        [DisplayName("Date of Birth")]
        public DateTime dateOfBirth { get; set; }

        [DisplayName("Age ")]
        public string age { get; set; }

        [DisplayName("Adrress 1 ")]
        public string address_1 { get; set; }

        [DisplayName("Adrress 2")]

        public string address_2 { get; set; }

        [DisplayName("Adrress 3")]
        public string address_3 { get; set; }
        [DisplayName("Adrress 4 ")]
        public string address_4 { get; set; }

        [DisplayName("Country Code")]
        public string country { get; set; }

        [DisplayName("Language ")]
        public string language { get; set; }

        [DisplayName("Balance ")]
        public decimal balance { get; set; }
        public char active { get; set; }
        public DateTime created_date { get; set; }
    }
}
