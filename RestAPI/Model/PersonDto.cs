namespace RestAPI.Model
{
    public class PersonDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonId { get; set; }

        public AddressDto Address { get; set; }
    }
}
