using FastEndpoints;
using FluentValidation;
using TBP.Core.DTOs;

namespace TBP.Presentation.Endpoints.LoginEndpoint.login
{
    internal sealed class Request
    {
        public string Username { get; set; }

        public string PasswordHash { get; set; }
        internal sealed class Validator : Validator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.Username)
                    .NotEmpty()
                    .WithMessage("Username must be provided.");
                RuleFor(x => x.PasswordHash)
                    .NotEmpty()
                    .WithMessage("Password must be provided.");
            }
        }
    }

    internal sealed class Response
    {
        public string Token { get; set; }
        public string ErrorMessage { get; set; }
    }
}