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

        public List<UserType> UserTypes { get; private set; } = new();

        public Picture? Picture { get; set; }


        public static User Create(FirstName firstName,
            LastName lastName,
            Email email,
            Username username,
            Password password,
            Address address,
            Birthday birthday,
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
                picture);


            return user;
        }

     
    }
}
