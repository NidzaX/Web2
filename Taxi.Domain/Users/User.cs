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

        public FirstName FirstName { get; private set; }

        public LastName LastName { get; private set; }

        public Email Email { get; private set; }

        public Username Username { get; private set; }

        public Password Password { get; private set; }

        public Address Address { get; private set; }    

        public Birthday Birthday { get; private set; }

        public UserType UserType { get; private set; } 

        public Picture? Picture { get; private set; }

        public Verified Verified { get; private set; }

        public static User Create(FirstName firstName,
            LastName lastName,
            Email email,
            Username username,
            Password password,
            Address address,
            Birthday birthday,
            UserType userType,
            Picture picture)
        {
            var user = new User(Guid.NewGuid(),
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
