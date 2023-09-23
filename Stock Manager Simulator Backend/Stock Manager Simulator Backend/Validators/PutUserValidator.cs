using FluentValidation;
using Stock_Manager_Simulator_Backend.Constans;
using Stock_Manager_Simulator_Backend.Dtos;
using Stock_Manager_Simulator_Backend.Repositories.Interfaces;

namespace Stock_Manager_Simulator_Backend.Validators
{
    public class PutUserValidator : AbstractValidator<PutUserDto>
    {
        public PutUserValidator(IUserRepository _userRepository)
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage(ErrorConstans.NAME_IS_REQUIRED)
                .WithMessage(ErrorConstans.THERE_IS_USER_WITH_THIS_USERNAME);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ErrorConstans.EMAIL_IS_REQUIRED)
                .EmailAddress().WithMessage(ErrorConstans.INVALID_EMAIL_FORMAT)
                .WithMessage(ErrorConstans.THERE_IS_USER_WITH_THIS_EMAIL);

            RuleFor(x => x.BirthOfDate)
                .NotEmpty().WithMessage(ErrorConstans.BIRTH_OF_DATE_IS_REQUIRED)
                .NotNull().WithMessage(ErrorConstans.BIRTH_OF_DATE_IS_REQUIRED);

            RuleFor(x => x.IsMan)
                .NotNull().WithMessage(ErrorConstans.GENDER_IS_REQUIRED);

            RuleFor(x => x.Lastname)
                .NotEmpty().WithMessage(ErrorConstans.LASTNAME_IS_REQUIRED);

            RuleFor(x => x.Firstname)
                .NotEmpty().WithMessage(ErrorConstans.FIRSTNAME_IS_REQUIRED);
        }
    }
}
