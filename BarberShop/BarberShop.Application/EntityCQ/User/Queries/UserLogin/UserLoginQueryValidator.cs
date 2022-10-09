using FluentValidation;

namespace BarberShop.Application.EntitiesCQ.User.Queries.UserLogin
{
    public class UserLoginQueryValidator : AbstractValidator<UserLoginQuery>
    {
        public UserLoginQueryValidator()
        {
            RuleFor(userLoginQuery => userLoginQuery.Phone).NotEmpty();
            RuleFor(userLoginQuery => userLoginQuery.Password).NotEmpty();
        }
    }
}
