using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;

namespace HomeWork_12
{
    public class ContactManager<T> where T : IContact
    {
        // Уникальная коллекция контактов, ключ = номер телефона
        private Dictionary<string, string> contacts_ = new Dictionary<string, string>();
        public void AddContact(T contact) // Метод добавления нового контакта
        {
            try
            {
                if (contacts_.ContainsKey(contact.PhoneNumber)) // Исключительная ситуация
                    throw new InvalidValueException("Контакт с таким номером уже существует");                
                contacts_.Add(contact.PhoneNumber, contact.Name);                
            }
            catch (InvalidValueException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nОшибка добавления нового контакта: {ex.Message}");
                Console.ForegroundColor= ConsoleColor.White;
            }            
        }
        public void RemoveContact(string phoneNumber) // Метод удаления контакта
        {
            if (contacts_.ContainsKey(phoneNumber))
            {
                contacts_.Remove(key: phoneNumber);
                Console.WriteLine("Контакт успешно удалён!");
            }                
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nКонтакта с таким номером в телефонной книге нет!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public Dictionary<string,string> FindName(string name) // Метод поиска контактов по имени
        {
            return contacts_.Where(c => c.Value == name).ToDictionary(c => c.Key, c => c.Value);
        }
        public Contact FindPhoneNumber(string phoneNumber) // Метод поиска контактов по номеру телефона
        {
            Contact contact = new Contact();
            if (contacts_.TryGetValue(phoneNumber, out string name))
            {
                contact.PhoneNumber = phoneNumber;
                contact.Name = name;
            }
            else
                contact.PhoneNumber = "-1";
            return contact;
        }
        public void SortName() // Метод сортирвоки контактов в алфавитном порядке
        {
            contacts_ = contacts_.OrderBy(c => c.Value[0]).ToDictionary(c => c.Key, c => c.Value);
            Console.WriteLine("\nСортировка успешно завершена!");
        }
        public void Print() // Метод вывода списка контактов в консоль
        {
            foreach (var contact in contacts_) 
                Console.WriteLine($"Имя: {contact.Value}, номер телефона: {contact.Key}");
        }
        public void Clear() { contacts_.Clear(); } // Метод очистки содержимого списка контактов
        public void SaveXML(string path) // Метод сохранения данных в xml-документе
        {
            XElement doc = new XElement ("Contacts", // Создаём новый xml-документ с root-корневым каталогом "contacts"
                from el in contacts_ 
                select new XElement("Contact", // Добавляем новую подпапку "Contact" в корневую
                new XAttribute("Name", el.Value),
                new XAttribute("PhoneNumber", el.Key)
                ));
            doc.Save(path);
        }
        public IEnumerable<IContact> DownloadXML(string path) // Метод загрузки данных из xml-документа
        {            
            XDocument doc = XDocument.Load(path); // Создаём объект для загрузки из xml-файла
            // В переменной result формируем список объектов Contact, считанных из xml-файла
            var result = doc.Descendants("Contact")
                .Select(el => new Contact
                {
                    PhoneNumber = el.Attribute("PhoneNumber").Value,
                    Name = el.Attribute("Name").Value                    
                }
                ).ToList();
            return result;
        }
        public void SaveJSON(string path) // Метод сохранения данных в json-документе
        {
            List<Contact> tmp = new List<Contact>();
            foreach (var contact in contacts_)
            {                
                tmp.Add(new Contact { Name = contact.Value, PhoneNumber = contact.Key });
            }
            string json = JsonConvert.SerializeObject(tmp);
            File.WriteAllText(path, json);
        }
        public IEnumerable<IContact> DownloadJSON(string path) // Метод загрузки данных из json-документа
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<Contact>>(json);
        }
    }
}
