namespace Mobile_3ilPark.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
		public string? ConfirmPassword { get; set; }
		public string PhoneNumber { get; set; }
        = string.Empty; 

    }
}
