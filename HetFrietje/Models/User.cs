namespace HetFrietje.Models
{
    public class User
    {
        public string Username { get; private set; }
        public string Name { get; private set; }
        public List<Order>? Orders { get; private set; }
        public PermissionLevel PermissionLevel { get; private set; }
    }
}
