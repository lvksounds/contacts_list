using AgendaContatos.Back.Models;

namespace AgendaContatos.Back.services.Contacts
{
    public interface IContactsService
    {
        Task<string> CreateContact(Contact contact, string userId);
        Task<string> DeleteContact(string contactId);
        Task<Contact> GetSpecificContact(string contactId);
        Task<IEnumerable<Contact>> GetUserContacts(string userId);
        Task<string> UpdateContact(Contact contact);
    }
}