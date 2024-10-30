// --------------------------------------------------------------------------------------------------
// <copyright file="UpdateContactRequestValidator.cs" company="LocalFriendz">
// Copyright (c) LocalFriendz. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentValidation;
using LocalFriendzApi.Application.Request;

namespace LocalFriendzApi.Application.Validations
{
    public class UpdateContactRequestValidator : AbstractValidator<UpdateContactRequest>
    {
        public UpdateContactRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(2).WithMessage("Name must be at least 2 characters long.")
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Phone number must be a valid international phone number.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("A valid email is required.")
                .EmailAddress().WithMessage("A valid email is required.");

            RuleFor(x => x.AreaCode)
                .NotEmpty().WithMessage("CodeRegion is required.")
                .MinimumLength(2).WithMessage("CodeRegion must be at least 2 characters long.");

            RuleFor(x => x)
                .Must(HaveUniquePhoneOrEmail).WithMessage("A contact with the same phone or email already exists.");
        }

        private bool HaveUniquePhoneOrEmail(UpdateContactRequest request)
        {
            // Simulate a uniqueness check. In a real scenario, you might query your database here.
            var existingContacts = new List<CreateContactRequest>
            {
                new CreateContactRequest { Phone = "+1234567890", Email = "existing@example.com" }
            };

            return !existingContacts.Any(c => c.Phone == request.Phone || c.Email == request.Email);
        }
    }
}
