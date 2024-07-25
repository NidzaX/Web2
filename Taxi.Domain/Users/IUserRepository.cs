﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        void Add(User user);

    }
}