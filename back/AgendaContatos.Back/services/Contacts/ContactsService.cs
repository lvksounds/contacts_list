using AgendaContatos.Back.data;
using AgendaContatos.Back.helpers;
using AgendaContatos.Back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgendaContatos.Back.services.Contacts
{
    public class ContactsService : IContactsService
    {
        private readonly DataBaseContext _dataBaseContext;

        public ContactsService(DataBaseContext dataBase)
        {
            _dataBaseContext = dataBase;
        }

        public async Task<IEnumerable<dynamic>> GetUserContacts(string userId)
        {

            User user = await _dataBaseContext.Users.FindAsync(userId);
            
            if (user == null) return Enumerable.Empty<Contact>();

            try
            {
                var contacts = _dataBaseContext.Contacts.Where(u => u.UserId == user.UserId).ToList();
                foreach (var contact in contacts)
                {
                    if(contact.profileImgBytes != null) contact.profileImg = "data:image/jpeg;base64," + Convert.ToBase64String(contact.profileImgBytes);
                }
                user.UserContacts = contacts;

                return user.UserContacts;
            }
            catch (Exception ex) 
            {
                return new List<Exception> { ex };
            }
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
            try
            {
                if (contact.profileImg != null)
                {
                    byte[] imageBytes = Base64Converter.ConvertFromBase64(contact.profileImg);
                    newContact.profileImgBytes = imageBytes;
                    newContact.profileImg = null;
                }
            }
            catch (Exception ex) { return ex.Message; };
           

            try
            {
                await _dataBaseContext.Contacts.AddAsync(newContact);
                await _dataBaseContext.SaveChangesAsync();
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

        
        public async Task<string> DeleteContact(string contactId, string userId)
        {
            Contact contact = await _dataBaseContext.Contacts.FirstOrDefaultAsync(c => c.ContactId == contactId && c.UserId == userId);

            if (contact != null)
            {
                try
                {
                    _dataBaseContext.Contacts.Remove(contact);
                    await _dataBaseContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return "ContactDeleted";
        }

        private string GetNewContactId()
        {
            Random idIncrement = new Random();
            int aleatoryNumber = idIncrement.Next(0, 100);
            string id = (_dataBaseContext.Contacts.Count() + 1).ToString() + aleatoryNumber;

            return id;
        }
    }
}
