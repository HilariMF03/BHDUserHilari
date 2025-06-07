namespace Application.Dtos
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<PhoneRequest> Phones { get; set; }
    }

    public class PhoneRequest
    {
        public string Number { get; set; }
        public string CityCode { get; set; }
        public string ContryCode { get; set; }
    }

}
