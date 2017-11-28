using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOSWeek1
{
    public class People
    {
        private List<string> _persons;
        
        public People ( ) {
            this._persons = new List<string>() {
                "Arnar Freyr",
                "Edda Steinunn",
                "Darri Valgardsson",
                "Skuli Arnarsson",
                "Axel Bjornsson",
                "Sigurdur Marteinn",
                "Smari Bjorn",
                "Andri Karel"
            };
        }
        public List<string> Person => this._persons; // Sama og get{ return this._persons }; bara einfaldara
    }
}
