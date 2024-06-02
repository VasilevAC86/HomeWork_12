using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_12
{
    public class Contact:IContact
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        /*public void Print() // Метод вывода данных о контакте в консоль
        {
            Console.WriteLine($"Имя: {Name}, номер телефона: {PhoneNumber}");
        }*/
    }
}
