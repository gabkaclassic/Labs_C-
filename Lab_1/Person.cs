using System;
using System.Globalization;

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

        public override bool Equals(object? obj)
        {
            if (obj is not Person other)
                return false;

            return lastname.Equals(other.lastname)
                   && firstname.Equals(other.firstname)
                   && Birthday.Equals(other.Birthday);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(firstname, lastname, birthday);
        }
        public virtual object DeepCopy()
        {

            return new Person
            {
                birthday = DateTime.Parse(birthday.ToString(CultureInfo.InvariantCulture)),
                firstname = string.Copy(firstname),
                lastname = string.Copy(lastname)
            };
        }

        public static bool operator==(Person person1, Person person2)
        {
            return person1.Equals(person2);
        }

        public static bool operator !=(Person person1, Person person2)
        {
            return !(person1 == person2);
        }
    }
}