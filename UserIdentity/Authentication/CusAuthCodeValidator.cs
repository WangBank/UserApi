using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Threading.Tasks;
using UserIdentity.Services;

namespace UserIdentity.Authentication
{
    public class CusAuthCodeValidator : IExtensionGrantValidator
    {
        public string GrantType => "wangbank";
        public IAuthCodeService authCodeService;
        public IUserService userService;

        public CusAuthCodeValidator(IAuthCodeService _authCodeService, IUserService _userService)
        {
            authCodeService = _authCodeService;
            userService = _userService;
        }
        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var phone = context.Request.Raw["phone"];
            var authCode = context.Request.Raw["authCode"];
            var errorValidationResult = new GrantValidationResult(TokenRequestErrors.InvalidGrant);

            //检测是否为空
            if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(authCode))
            {
                context.Result = errorValidationResult;
                return;
            }

            //验证码
            if (!await authCodeService.Validate(phone, authCode))
            {
                context.Result = errorValidationResult;
                return;
            }

            //完成用户注册
            int userId = await userService.CheckOrCreate(phone);
            if (userId <= 0)
            {
                context.Result = errorValidationResult;
                return;
            }

            context.Result = new GrantValidationResult(userId.ToString(), GrantType);
        }
    }
}
