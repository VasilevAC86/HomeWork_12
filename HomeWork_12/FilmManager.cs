using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HomeWork_12
{
    public class FilmManager
    {
        private List<IFilm> films_ = new List<IFilm>(); // Поле для хранения коллеции фильмов
        public void AddFilm(IFilm obj) // Метод добавление фильма
        {
            films_.Add(obj);
        }
        public void RemoveFilm(string name) // Метод удаления фильма по названию
        {
            films_.RemoveAll(x => x.Name.ToLower() == name.ToLower());
        }
        public IEnumerable<IFilm> FindFilmName(string name) // Метод поиска фильмов по названию
        {
            return films_.Where(f => f.Name.ToLower() == name.ToLower()).ToList();
        }
        public IEnumerable<IFilm> FindFilmGenre(string genre) // Метод поиска фильмов по жанру
        {
            return films_.Where(f => f.Genre.ToLower() == genre.ToLower()).ToList();
        }
        public void SaveXML(string path) // Метод сохранения коллекции фильмов в xml-файле
        {
            XElement doc_xml = new XElement("Films", // "Фильмы" - корневой каталог (root)
                from f in films_
                select new XElement("Film", // "Фильм" - дочька root'а (вложенный в корень каталог)
                new XAttribute("Name", f.Name),
                new XAttribute("Year", f.Year),
                new XAttribute("Regisseur", f.Regisseur),
                new XAttribute("Genre", f.Genre)
                ));
            doc_xml.Save(path);
        }
        public IEnumerable<IFilm> DownloadXML(string path) // Метод загрузки фильмов из xml-файла
        {
            XDocument doc_xml = XDocument.Load(path); // Создаём объект класса XDocument для загрузки данных из xml-файла
            // В переменной result формируем список объектов Contact, считанных из xml-файла
            var result = doc_xml.Descendants("Film")
                .Select(f => new Film 
                { 
                    Name = f.Attribute("Name").Value,
                    Year = int.Parse(f.Attribute("Year").Value),
                    Regisseur = f.Attribute("Regisseur").Value,
                    Genre = f.Attribute("Genre").Value
                }).ToList();
            return result;
        }
        public void SaveJSON(string path) // Метод сохранения коллекции фильмов в json-файле
        {
            string json = JsonConvert.SerializeObject(films_);
            File.WriteAllText(path, json);
        }
        public IEnumerable<IFilm> DownloadJSON(string path) // Метод загрузки фильмов из json-файла
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<Film>>(json);
        }
        public void Clear() { films_.Clear(); } // Метод очистки коллекции фильмов
        public IEnumerable<IFilm> SortYear() // Метод сортировки по возрастанию года
        {
            return films_.OrderBy(f => f.Year).ToList();
        }
    }
}
