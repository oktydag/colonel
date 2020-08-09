namespace Colonel.Shopping.Models.User
{
    public class UserResponseModel
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public long PhoneNumber { get; set; }

        public int Age { get; set; }

        public bool IsActive { get; set; }
    }
}
