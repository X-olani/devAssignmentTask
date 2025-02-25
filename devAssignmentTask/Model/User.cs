namespace devAssignmentTask.Model
{
    public class User
    {
        public int Id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public DateTime dateOfBirth { get; set; }
        public string age { get; set; }

        public string address_1 { get; set; }
        public string address_2 { get; set; }
        public string address_3 { get; set; }
        public string address_4 { get; set; }

        public string country { get; set; }
        public string language { get; set; }

        public decimal balance { get; set; }
        public char active { get; set; }
        public DateTime created_date { get; set; }
    }
}
