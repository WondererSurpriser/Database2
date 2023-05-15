using Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace EntityFramework
{
    class Program
    {
        
        static void Main()
        {
            Console.WriteLine("Welcome to my first database! What do you want to do?");
            Methods.ShowMenu();
            
            //Все манипуляции с базой данных вынесены в отдельные методы для улучшения читаемости кода.
            //Таймеры подставлены непосредственно в те участки кода, где данные пользователем уже введены,
            //это сделано для корректного замера времени выполнения определённых действий.
        }
    }
}



