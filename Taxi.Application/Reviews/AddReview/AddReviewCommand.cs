﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Application.Abstractions.Messaging;

namespace Taxi.Application.Reviews.AddReview
{
    public sealed record AddReviewCommand(
        string UserEmail,
        Guid RiderId,
        int Rating,
        string Comment) : ICommand;

}