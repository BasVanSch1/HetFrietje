namespace HetFrietje.Models
{
    public class User
    {
        public int UserId { get; private set; }
        public string Name { get; private set; }
        public List<Order> Orders { get; private set; }
        public PermissionLevel PermissionLevel { get; private set; }
    }
}
