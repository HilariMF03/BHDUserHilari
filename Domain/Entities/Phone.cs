
namespace Domain.Entities
{
    public class Phone
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Number { get; set; }
        public string CityCode { get; set; }
        public string ContryCode { get; set; }
        public Guid UserId { get; set; }
        public Users User { get; set; }
    }
}
