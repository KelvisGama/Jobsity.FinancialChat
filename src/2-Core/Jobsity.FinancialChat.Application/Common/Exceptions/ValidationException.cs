using FluentValidation.Results;
using Jobsity.FinancialChat.Application.Common.Models;
using System;
using System.Collections.Generic;

namespace Jobsity.FinancialChat.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Errors = new List<Error>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(new Error
                {
                    ErrorCode = int.TryParse(failure.ErrorCode, out var errorCode) ? errorCode : 0,
                    PropertyName = failure.PropertyName,
                    ErrorMessage = failure.ErrorMessage
                });
            }
        }

        public List<Error> Errors { get; }
    }
}
