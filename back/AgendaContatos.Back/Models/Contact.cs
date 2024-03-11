namespace AgendaContatos.Back.Models
{
    public class Contact
    {
        public Contact() { }
        public Contact(string userId, Contact contact) 
        {
            UserId = userId;
            Name = contact.Name;
            Phone = contact.Phone;
            
        }
        public string UserId { get; set; }
        public string? ContactId { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string Phone { get; set; }
        public byte[]? profileImg { get; set; }
        public bool? isFavorite { get; set; }

    }
}
