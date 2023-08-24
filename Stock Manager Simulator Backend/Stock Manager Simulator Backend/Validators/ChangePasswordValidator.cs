using FluentValidation;
using Stock_Manager_Simulator_Backend.Constans;
using Stock_Manager_Simulator_Backend.Dtos;

namespace Stock_Manager_Simulator_Backend.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage(ErrorConstans.PASSWORD_IS_REQUIRED)
                .MinimumLength(8).WithMessage(ErrorConstans.PASSWORD_IS_TOO_SHORT)
                .Matches("[A-Z]").WithMessage(ErrorConstans.PASSWORD_MUST_HAVE_UPPERCASE_LETTER)
                .Matches("[a-z]").WithMessage(ErrorConstans.PASSWORD_MUST_HAVE_LOWERCASE_LETTER)
                .Matches("[0-9]").WithMessage(ErrorConstans.PASSWORD_MUST_HAVE_NUMBER);
        }
    }
}
