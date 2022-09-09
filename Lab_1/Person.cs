using System;

namespace Lab_1
{

    public class Person
    {
        private string firstname;
        public string Firstname
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                    firstname = value;
            }
            get => firstname;
        }

        private string lastname;
        public string Lastname
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                    lastname = value;
            }
            get => lastname;
        }

        private DateTime birthday;

        public DateTime Birthday
        {
            get => birthday;
            set => birthday = value;
        }

        public int BirthYear
        {
            get => Birthday.Year;
            set => Birthday.AddYears(value - Birthday.Year);
        }

        public Person()
        {
            lastname = "<Unknown>";
            firstname = "<Unknown>";
            Birthday = DateTime.Today;
        }

        public Person(string firstname, string lastname, DateTime birthday)
        {
            this.firstname = firstname;
            this.lastname = lastname;
            Birthday = birthday;
        }

        public override string ToString()
        {
            return $"Person: [Firstname: {Firstname}, Lastname: {Lastname}, Date of birth: {Birthday.Day}.{Birthday.Month}.{Birthday.Year}]";
        }

        public virtual string ToShortString()
        {
            return $"Person: [Firstname: {Firstname}, Lastname: {Lastname}]";
        }

    }
}