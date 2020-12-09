using System;
using FluentValidation.TestHelper;
using Jobsity.FinancialChat.Application.Messages.Commands.AddMessage;
using Jobsity.FinancialChat.Domain.Enums;
using Xunit;

namespace Jobsity.FinancialChat.Application.Tests.Messages.Commands.AddMessage
{
    public class AddMessageCommandValidatorTests
    {
        private readonly AddMessageCommandValidator _validator;

        public AddMessageCommandValidatorTests()
        {
            _validator = new AddMessageCommandValidator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(default(string))]
        public void Should_HaveValidationError_When_UserNameIsEmpty(string userName)
        {
            // arrange
            var command = new AddMessageCommand(userName, "body", DateTime.Now);

            // act
            var result = _validator.TestValidate(command);

            // assert
            result.ShouldHaveValidationErrorFor(x => x.UserName)
                .WithErrorCode(((int) ValidationErrorCode.MessageUserNameEmpty).ToString())
                .WithErrorMessage("The message User Name is required");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(default(string))]
        public void Should_HaveValidationError_When_BodyIsEmpty(string body)
        {
            // arrange
            var command = new AddMessageCommand("userName", body, DateTime.Now);

            // act
            var result = _validator.TestValidate(command);

            // assert
            result.ShouldHaveValidationErrorFor(x => x.Body)
                .WithErrorCode(((int)ValidationErrorCode.MessageBodyEmpty).ToString())
                .WithErrorMessage("The message Body is required");
        }

        [Theory]
        [InlineData(null)]
        public void Should_HaveValidationError_When_DateIsEmpty(DateTime when)
        {
            // arrange
            var command = new AddMessageCommand("userName", "body", when);

            // act
            var result = _validator.TestValidate(command);

            // assert
            result.ShouldHaveValidationErrorFor(x => x.When)
                .WithErrorCode(((int)ValidationErrorCode.MessageWhenEmpty).ToString())
                .WithErrorMessage("The message When date is required");
        }

    }
}