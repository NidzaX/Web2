using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Application.Abstractions.Messaging;

namespace Taxi.Application.Reviews.Commands.AddReview
{
    public sealed record AddReviewCommand(
        string UserEmail,
        Guid DriverId,
        int Rating,
        string Comment) : ICommand;

}
