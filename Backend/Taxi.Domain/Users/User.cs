using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Domain.Abstractions;

namespace Taxi.Domain.Users
{
    public sealed class User : Entity
    {
        private User(Guid id,
            FirstName firstName,
            LastName lasttName,
            Email email,
            Username username,
            Password password,
            Address address,
            Birthday birthday,
            UserType userType,
            Picture picture
            ) : 
            base(id)
        {
            FirstName = firstName;
            LastName = lasttName;
            Email = email;
            Username = username;
            Password = password;
            Address = address;
            Birthday = birthday;
            UserType = userType;
            Picture = picture;
        }

        private User()
        {

        }

        public FirstName FirstName { get; set; }

        public LastName LastName { get; set; }

        public Email Email { get;  set; }

        public Username Username { get;  set; }

        public Password Password { get; set; }

        public Address Address { get;  set; }    

        public Birthday Birthday { get;  set; }

        public UserType UserType { get;  set; } 

        public Picture? Picture { get;  set; }

        public Verified? Verified { get; set; } 

        public static User Create(
            FirstName firstName,
            LastName lastName,
            Email email,
            Username username,
            Password password,
            Address address,
            Birthday birthday,
            UserType userType,
            Picture picture)
        {
            var user = new User(
                Guid.NewGuid(),
                firstName,
                lastName,
                email,
                username,
                password,
                address,
                birthday,
                userType,
                picture);


            return user;
        }

     
    }
}
