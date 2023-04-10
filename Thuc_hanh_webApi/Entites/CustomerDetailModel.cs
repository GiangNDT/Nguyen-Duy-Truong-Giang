namespace Thuc_hanh_webApi.Entites
{
    public class CustomerDetailModel
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public int? Age { get; set; }

        public string? Gender { get; set; }

        public string? Address { get; set; }

        public string Username { get; set; } = null!;

        public int Status { get; set; }

        public string? Description { get; set; }
        public List<Production> Productions { get; set; }
    }
}
