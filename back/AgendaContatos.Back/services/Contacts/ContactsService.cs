using AgendaContatos.Back.data;
using AgendaContatos.Back.Models;

namespace AgendaContatos.Back.services.Contacts
{
    public class ContactsService : IContactsService
    {
        private readonly DataBaseContext _dataBaseContext;

        public ContactsService(DataBaseContext dataBase)
        {
            _dataBaseContext = dataBase;
        }

        public async Task<IEnumerable<Contact>> GetUserContacts(string userId)
        {
            return new List<Contact>();
        }

        public async Task<Contact> GetSpecificContact(string contactId)
        {
            return new Contact();
        }

        public async Task<string> CreateContact(Contact contact, string userId)
        {
            Contact newContact = contact;
            newContact.ContactId = GetNewContactId();
            newContact.UserId = userId;

            // fazer tratamento da imagem de perfil

            try
            {
                await _dataBaseContext.Contacts.AddAsync(newContact);
                return $"ContactCreated";
            }

            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public async Task<string> UpdateContact(Contact contact)
        {
            return "updateContact";
        }

        public async Task<string> DeleteContact(string contactId)
        {
            return "updateContact";
        }

        private string GetNewContactId()
        {
            string id = (_dataBaseContext.Contacts.Count() + 1).ToString();

            return id;
        }
    }
}
