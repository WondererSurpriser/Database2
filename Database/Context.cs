using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Context : DbContext
    {
        public Context() 
        {
            Database.EnsureCreated();   
        }
        public DbSet<Person> Persons { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=../../../MyLocalLibrary2.db ");
        }
        //создание самой базы данных или проверка её создания при повторном выполнении приложения и определение конфигурации подключения.
    }
}
