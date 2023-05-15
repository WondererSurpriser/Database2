using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly DateofBirth { get; set; }
        public string  Sex { get; set; }
    }
    //Данный класс определяет общий вид таблицы в базе данных.
}
