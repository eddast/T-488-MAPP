using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOSWeek1
{
    public class People
    {
        private List<Person> _persons;

        public People() {
            this._persons = new List<Person>();
            LoadPersons();
        }

        public List<Person> Person => this._persons; // Sama og get{ return this._persons }; bara einfaldara

        private void AddPerson(string name, int year, string imageName) {
            var person = new Person() {
                Name = name,
                BirthYear = year,
                ImageName = imageName
            }; this._persons.Add(person);
        }

        private void LoadPersons() {
            this.AddPerson("Edda Steinunn", 1995, "edda_steinunn");
            this.AddPerson("Another person", 1900, "another_person");
        }
    }
}
