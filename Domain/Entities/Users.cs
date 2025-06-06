namespace Domain.Entities
{
    public class Users
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Phone> Phones { get; set; } = new List<Phone>();
        public string Token { get; set; }
        public DateTime LastLogin { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Modified { get; set; }
    }
}
