namespace HetFrietje.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public List<Order>? Orders { get; set; }
        public PermissionLevel PermissionLevel { get; set; }
    }
}
