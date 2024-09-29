namespace HetFrietje.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public IList<Order>? Orders { get; set; }
        public PermissionLevel PermissionLevel { get;   set; }
    }
}
