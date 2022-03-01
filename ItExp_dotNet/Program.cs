using ItExp_dotNet.Models;
using ItExp_dotNet.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItExp_dotNet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ThisAppContext db = new ThisAppContext())
            {
                if (db.Users.Count() == 0)
                {
                    Console.WriteLine("Заполнение БД начальными данными.");

                    List<User> users = new List<User>()
                    {
                        new User
                        {
                            Id = Guid.NewGuid(),
                            Name = "Василий",
                            Age = 20
                        },
                        new User
                        {
                            Id = Guid.NewGuid(),
                            Name = "Дмитрий",
                            Age = 25
                        },
                        new User
                        {
                            Id = Guid.NewGuid(),
                            Name = "Александр",
                            Age = 18
                        },
                        new User
                        {
                            Id = Guid.NewGuid(),
                            Name = "Анатолий",
                            Age = 40
                        },
                        new User
                        {
                            Id = Guid.NewGuid(),
                            Name = "Михаил",
                            Age = 27
                        },
                        new User
                        {
                            Id = Guid.NewGuid(),
                            Name = "Виталий",
                            Age = 13
                        },
                        new User
                        {
                            Id = Guid.NewGuid(),
                            Name = "Вениамин",
                            Age = 31
                        },
                    };

                    db.Users.AddRange(users);
                    db.SaveChanges();

                    Console.WriteLine("БД заполнена. Нажмите любую клавишу для продолжения.");
                    Console.ReadKey();
                }

                try
                {
                    var dbConnectionString = ConfigurationManager.ConnectionStrings["ItExpDB"].ToString();

                    using (SqlConnection connection = new SqlConnection(dbConnectionString))
                    {
                        if (!Directory.Exists("TxtFiles"))
                        {
                            Console.WriteLine("Папка TxtFiles не найдена.");
                        }
                        else
                        {
                            var file = File.OpenText("TxtFiles/input.txt");

                            string queryString = file.ReadLine();
                            Console.WriteLine($"Для выполнения команды \"{queryString}\" нажмите любую клавишу.");
                            Console.ReadKey();

                            SqlCommand sqlCommand = new SqlCommand(queryString, connection);

                            connection.Open();

                            using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                            {
                                using (StreamWriter writer = new StreamWriter("TxtFiles/output.csv", false))
                                {
                                    while (sqlReader.Read())
                                    {
                                        writer.WriteLine($"{sqlReader["Name"]}, {sqlReader["Age"]}");
                                    }
                                }
                            }

                            connection.Close();

                            Console.WriteLine("Результат записан в файл \"output.csv\"");
                        }                        
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                Console.WriteLine("Для закрытия окна приложения нажмите любую клавишу...");
                Console.ReadKey();
            }
        }
    }
}
