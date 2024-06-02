using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_12
{
    public interface IFilm
    {
        string Name { get; set; } // Название фильма
        int Year { get; set; } // Год премьеры фильма
        string Regisseur { get; set; } // Режиссёр
        string Genre { get; set; } // Жанр
        void Print(); // Метод вывода фильма в консоль
    }
}
