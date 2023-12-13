using System.Collections.Generic;

namespace RESTfulEmployeesLibrary.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }

        public User Clone()
        {
            return new User
            {
                Name = Name,
                Email = Email,
                Gender = Gender,
                Status = Status
            };
        }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   Id == user.Id &&
                   Name == user.Name &&
                   Email == user.Email &&
                   Gender == user.Gender &&
                   Status == user.Status;
        }

        public override int GetHashCode()
        {
            int hashCode = 765261660;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Gender);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Status);
            return hashCode;
        }
    }
}
