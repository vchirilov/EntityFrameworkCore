using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace PR01_HelloApp
{
    class Program
    {
        public static void Main(string[] args)
        {

            using (ApplicationContext db = new ApplicationContext())
            {
                // пересоздадим базу данных
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                // создание и добавление моделей
                Student tom = new Student { Name = "Tom" };
                Student alice = new Student { Name = "Alice" };
                Student bob = new Student { Name = "Bob" };
                db.Students.AddRange(tom, alice, bob);

                Course algorithms = new Course { Name = "Алгоритмы" };
                Course basics = new Course { Name = "Основы программирования" };
                Course sql = new Course { Name = "SQL" };

                db.Courses.AddRange(algorithms, basics, sql);

                // добавляем к студентам курсы
                tom.Courses.Add(algorithms);
                tom.Courses.Add(basics);                
                alice.Courses.Add(algorithms);
                bob.Courses.Add(basics);

                sql.Students.AddRange(new Student[] { tom, bob });

                db.SaveChanges();
            }


            using (ApplicationContext db = new ApplicationContext())
            {
                var courses = db.Courses.Include(c => c.Students).ToList();
                // выводим все курсы
                foreach (var c in courses)
                {
                    Console.WriteLine($"Course: {c.Name}");
                    // выводим всех студентов для данного кура
                    foreach (Student s in c.Students)
                        Console.WriteLine($"Name: {s.Name}");
                    Console.WriteLine("-------------------");
                }
            }

            Console.WriteLine("Done");
            Console.Read();
        }
    }
}
