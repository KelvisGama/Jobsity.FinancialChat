using FluentValidation;
using Jobsity.FinancialChat.Domain.Enums;

namespace Jobsity.FinancialChat.Application.Messages.Commands.AddMessage
{
    public class AddMessageCommandValidator : AbstractValidator<AddMessageCommand>
    {
        public AddMessageCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithErrorCode(((int)ValidationErrorCode.MessageUserNameEmpty).ToString())
                .WithMessage("The message User Name is required");

            RuleFor(x => x.Body)
                .NotEmpty()
                .WithErrorCode(((int)ValidationErrorCode.MessageBodyEmpty).ToString())
                .WithMessage("The message Body is required");

            RuleFor(x => x.When)
                .NotEmpty()
                .WithErrorCode(((int)ValidationErrorCode.MessageWhenEmpty).ToString())
                .WithMessage("The message When date is required");
        }
    }
}
