using core.Utilities.Results;
using core.Utilities.Security.Jwt;
using Core.Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(String email);
        IDataResult<AccessToken> CreateAccessToken(User user);

    }
}
