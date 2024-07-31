using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Taxi.Application.Abstractions.Messaging;
using Taxi.Application.Users.VerifyDriver;
using Taxi.Domain.Abstractions;
using Taxi.Domain.Users;

namespace Taxi.Application.Users.VerifySeller
{
    internal sealed class VerifyDriverCommandHandler : ICommandHandler<VerifyDriverCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VerifyDriverCommandHandler(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork =  unitOfWork;
        }

        public async Task<Result> Handle(VerifyDriverCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetUserByEmailAsync(request.Email); // ???

            if(user == null)
            {
                throw new Exception("Invalid data");
            }

            user.Verified = new Verified(request.v); 

            MailMessage message = new MailMessage();
            message.From = new MailAddress("web2projekatadm@gmail.com");
            message.To.Add(new MailAddress(user.Email.Value));
            message.Subject = "Verifikacija naloga";
            if (request.v)
            {
                message.Body = "Vas nalog je uspesno verifikovan.";
            }
            else
            {
                message.Body = "Verifikacija odbijena.";
            }


            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true; // set SSL to true if required
            client.Credentials = new System.Net.NetworkCredential("nidzax1@gmail.com", "rrbfbizakitcrzsx");


            client.Send(message);

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();

            // rrbfbizakitcrzsx
            _userRepository.Update(user);

            return Result.Success();
        }
    }
}
