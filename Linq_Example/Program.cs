using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var lPerson = new List<Person>();
            PopulatePerson(lPerson);

            // LINQ
            var filtered = lPerson.Where(p => p.m_name.StartsWith("A") && p.m_gender == Person.Gender.Male);

            // LINQ to SQL
            var filtered2 = from person in lPerson
                            where person.m_name.StartsWith("A") &&
                                  person.m_gender == Person.Gender.Male
                            select person;
            // filtered and filtered2 are equal

            foreach (var person in filtered2)
            {
                Console.WriteLine(person.ToString() + '\n');
            }

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }

        private static void PopulatePerson(List<Person> lPerson)
        {
            lPerson.Add(new Person()
            {
                m_name = "Jacek",
                m_lastName = "Klepacki",
                m_birthDate = new DateTime(1994, 7, 5),
                m_gender = Person.Gender.Male
            });

            lPerson.Add(new Person()
            {
                m_name = "Szczepan",
                m_lastName = "Pecio",
                m_birthDate = new DateTime(1992, 8, 15),
                m_gender = Person.Gender.Male
            });

            lPerson.Add(new Person()
            {
                m_name = "Anna",
                m_lastName = "Jakaśtam",
                m_birthDate = new DateTime(1984, 2, 25),
                m_gender = Person.Gender.Female
            });

            lPerson.Add(new Person()
            {
                m_name = "Adam",
                m_lastName = "Rogacz",
                m_birthDate = new DateTime(1994, 11, 25),
                m_gender = Person.Gender.Male
            });

            lPerson.Add(new Person()
            {
                m_name = "Patryk",
                m_lastName = "Milczewski",
                m_birthDate = new DateTime(1993, 1, 2),
                m_gender = Person.Gender.Male
            });

            lPerson.Add(new Person()
            {
                m_name = "Andrzej",
                m_lastName = "Nunez",
                m_birthDate = new DateTime(1968, 12, 12),
                m_gender = Person.Gender.Male
            });

            lPerson.Add(new Person()
            {
                m_name = "Joanna",
                m_lastName = "Malinowska",
                m_birthDate = new DateTime(1973, 4, 29),
                m_gender = Person.Gender.Female
            });
        }
    }

    class Person
    {
        public enum Gender
        {
            Male,
            Female
        }

        public string m_name { get; set; }
        public string m_lastName { get; set; }
        public DateTime m_birthDate { get; set; }
        public Gender m_gender { get; set; }

        public override string ToString()
        {
            string val = ($"Name: {m_name}");
            val += ($"\nLast Name: {m_lastName}");
            val += ($"\nBirth Date: {m_birthDate.ToString("dd-MM-yyyy")}");
            val += ($"\nGender: {m_gender}");

            return val;
        }
    }
}
