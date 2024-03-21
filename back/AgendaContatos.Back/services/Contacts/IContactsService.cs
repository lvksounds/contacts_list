using AgendaContatos.Back.Models;

namespace AgendaContatos.Back.services.Contacts
{
    public interface IContactsService
    {
        Task<string> CreateContact(Contact contact, string userId);
        Task<string> DeleteContact(string contactId,string userId);
        Task<Contact> GetSpecificContact(string contactId);
        Task<IEnumerable<dynamic>> GetUserContacts(string userId);
        Task<string> UpdateContact(Contact contact);
    }
}