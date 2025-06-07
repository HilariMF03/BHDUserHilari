namespace Application.Dtos
{
    public class RegisterResponse
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public DateTime LastLogin { get; set; }
        public string Token { get; set; }
        public bool IsActive { get; set; }

        public bool HasError { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
