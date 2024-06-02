using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_12
{
    public class Film:IFilm
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public string Regisseur { get; set; }
        public string Genre { get; set; }
        public void Print() // Метод вывода фильма в консоль
        {
            Console.WriteLine($"Название: {Name}, жанр: {Genre}, год премьеры: {Year}, режиссёр: {Regisseur}");
        }        
    }
}
