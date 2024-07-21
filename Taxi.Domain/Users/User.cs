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
            LasttName lasttName,
            Email email) : 
            base(id)
        {
            FirstName = firstName;
            LastName = lasttName;
            Email = email;
        }

        public FirstName FirstName { get; private set; }

        public LasttName LastName { get; private set; }

        public Email Email { get; private set; }


        public static User Create(FirstName firstName,
            LasttName lastName,
            Email email)
        {
            var user = new User(Guid.NewGuid(), firstName, lastName, email);

            return user;
        }
    }
}
