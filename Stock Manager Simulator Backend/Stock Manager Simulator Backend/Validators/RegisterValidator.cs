using FluentValidation;
using Stock_Manager_Simulator_Backend.Constans;
using Stock_Manager_Simulator_Backend.Dtos;
using Stock_Manager_Simulator_Backend.Repositories;

namespace Stock_Manager_Simulator_Backend.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator(IUserRepository _userRepository)
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage(ErrorConstans.NAME_IS_REQUIRED)
                .Must(_userRepository.WithThisUsernameThereIsNoUser)
                .WithMessage(ErrorConstans.THERE_IS_USER_WITH_THIS_USERNAME);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ErrorConstans.EMAIL_IS_REQUIRED)
                .EmailAddress().WithMessage(ErrorConstans.INVALID_EMAIL_FORMAT)
                .Must(_userRepository.WithThisEmailThereIsNoUser)
                .WithMessage(ErrorConstans.THERE_IS_USER_WITH_THIS_EMAIL);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ErrorConstans.PASSWORD_IS_REQUIRED)
                .MinimumLength(8).WithMessage(ErrorConstans.PASSWORD_IS_TOO_SHORT)
                .Matches("[A-Z]").WithMessage(ErrorConstans.PASSWORD_MUST_HAVE_UPPERCASE_LETTER)
                .Matches("[a-z]").WithMessage(ErrorConstans.PASSWORD_MUST_HAVE_LOWERCASE_LETTER)
                .Matches("[0-9]").WithMessage(ErrorConstans.PASSWORD_MUST_HAVE_NUMBER);

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage(ErrorConstans.CONFIRMPASSWORD_IS_REQUIRED)
                .Equal(x => x.Password).WithMessage(ErrorConstans.CONFIRMPASSWORD_IS_NOT_MATCHED_WITH_PASSWORD);

            RuleFor(x => x.BirtOfDate)
                .NotEmpty().WithMessage(ErrorConstans.BIRTH_OF_DATE_IS_REQUIRED);

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage(ErrorConstans.GENDER_IS_REQUIRED);

            RuleFor(x => x.Lastname)
                .NotEmpty().WithMessage(ErrorConstans.LASTNAME_IS_REQUIRED);

            RuleFor(x => x.Firstname)
                .NotEmpty().WithMessage(ErrorConstans.FIRSTNAME_IS_REQUIRED);

        }
    }
}
