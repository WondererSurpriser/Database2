using Database;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EntityFramework
{
    public class Methods
    {
        public static void AddPerson()
        {
            Console.WriteLine("Enter full name: ");
            string name = Console.ReadLine();
            while (name == null || name == "")
            {
                Console.WriteLine("Name can't be null.");
                Console.WriteLine("Enter full name:");
                name = Console.ReadLine();
            }

            Console.WriteLine("Enter sex(Male/Female): ");
            string sex = Console.ReadLine();
            while (sex != "Male" && sex != "Female")
            {
                Console.WriteLine("Incorrect data, try again...");
                sex = Console.ReadLine();
            }

            DateOnly date;
            string dateInput;
            do
            {
                Console.WriteLine("Enter correct birthday: ");
                dateInput = Console.ReadLine();
            }
            while (!DateOnly.TryParseExact(dateInput, "dd.MM.yyyy", null, DateTimeStyles.None, out date));

            using (Context context = new Context())
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                Person person = new Person { Name = name, DateofBirth = date, Sex = sex };
                context.Persons.Add(person);
                context.SaveChanges();
                Console.WriteLine("Person successfully added!:)");
                stopwatch.Stop();
                Console.WriteLine($"{stopwatch.ElapsedMilliseconds} milliesecods");
                ShowMenu();
            };

        }
        public static void ShowList()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("List of persons:\n");
            using (Context context = new Context())
            {
                var persons = context.Persons.ToList();
                foreach (Person person in persons)
                {
                    Console.WriteLine($"ID: {person.Id};\nName: {person.Name};\nBirthday: {person.DateofBirth};");
                    Console.WriteLine();
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds} milliesecods");
            ShowMenu();
        }
        public static void Search()
        {
            Console.WriteLine("Enter person's Name:");
            string? name = Console.ReadLine();
            while (name == null || name == "")
            {
                Console.WriteLine("Name can't be null.");
                Console.WriteLine("Enter person's Name:");
                name = Console.ReadLine();
            }

            Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                using (Context context = new Context())
                {
                    var persons = context.Persons.Where(p => p.Name.Contains(name)).ToList();
                    if (persons.Count != 0)
                    {
                        foreach (Person person in persons)
                        {
                            Console.WriteLine($"ID: {person.Id};\nName: {person.Name};\nBirthday: {person.DateofBirth};");
                        }
                        stopwatch.Stop();
                        Console.WriteLine($"{stopwatch.ElapsedMilliseconds} milliesecods");


                        Console.WriteLine("What do you want to do?\n1.Remove.\n2.Change.\n3.Exit to menu.");
                        bool choice = int.TryParse(Console.ReadLine(), out int option);
                        while (option != 1 && option != 2 && option != 3)
                        {
                            choice = int.TryParse(Console.ReadLine(), out option);
                        }
                        switch (option)
                        {
                            case 1:
                                RemovePerson();
                                break;
                            case 2:
                                Change();
                                break;
                            case 3:
                                ShowMenu();
                                break;
                        }
                    }
                    else
                    {
                    Console.WriteLine("No results!\n1.Try again.\n2.Back to menu.");
                    bool choice = int.TryParse(Console.ReadLine(),out int option);
                     while (option != 1 && option != 2 || !choice)
                     {
                        Console.WriteLine("Incorrect option, try again.");
                        choice = int.TryParse(Console.ReadLine(), out option);
                     }
                    switch (option)
                    {
                        case 1:
                            Search();
                            break;
                        case 2:
                            ShowMenu();
                            break;
                    }
                    }
                
            }
        }
        public static void RemovePerson()
        {
            Console.WriteLine("Enter person's Id:");
            var id = int.Parse(Console.ReadLine());
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Context context = new Context();
            Person person = context.Persons.Find(id);
            context.Persons.Remove(person);
            context.SaveChanges();
            Console.WriteLine("Person removed!");
            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds} milliesecods");
            ShowMenu();

        }
        public static void Change()
        {
            Console.WriteLine("Enter person's Id:");
            var id = int.Parse(Console.ReadLine());
            Context context = new Context();
            Person person = context.Persons.Find(id);
            Console.WriteLine(person.Name);
            Console.WriteLine("Enter new name:");
            string? newName = Console.ReadLine();
            while (newName == null || newName == "")
            {
                Console.WriteLine("Name can't be null.");
                Console.WriteLine("Enter new name:");
                newName = Console.ReadLine();
            }
                Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            if (person.Name != null)
            {
                person.Name = newName;
            }
            context.SaveChanges();
            Console.WriteLine("Person changed!");
            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds} milliesecods");
            ShowMenu();
        }
        public static void RemoveAll()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Context context = new Context();
            context.RemoveRange(context.Persons);
            context.SaveChanges();
            Console.WriteLine("Data removed!");
            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds} milliesecods");
            ShowMenu();
        }
        public static void ShowMenu()
        {
            Console.WriteLine("Options:\n1.Show list.\n2.Add peerson.\n3.Find person.\n4.Remove all data.\n0.Exit.");
            bool choice = int.TryParse(Console.ReadLine(), out int option);
            while (option < 0 || option > 4 || !choice )
            {
                Console.WriteLine("Incorrect option, try again.");
                choice = int.TryParse(Console.ReadLine(), out option);
            }

            switch (option)
            {
                case 1:
                    ShowList();
                    break;
                case 2:
                    AddPerson();
                    break;
                case 3:
                    Search();
                    break;
                case 4:
                    RemoveAll();
                    break;
                case 0:
                    break;
            }
        }
    }
}
