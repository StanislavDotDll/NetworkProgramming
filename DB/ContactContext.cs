namespace DB
{
    using System.Data.Entity;

    public class ContactContext : DbContext
    {
        public ContactContext()
            : base("name=ContactContext")
        {
        }
        public DbSet<Contact> Contacts { get; set; }
    }
}