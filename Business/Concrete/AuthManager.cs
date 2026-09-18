using Business.Abstract;
using Business.Constants;
using core.Utilities.Results;
using core.Utilities.Security.Hashing;
using core.Utilities.Security.Jwt;
using Core.Entities.Concrete;
using Entities.Dtos;
using Microsoft.Identity.Client.NativeInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(ITokenHelper tokenHelper, IUserService userService)
        {
            _tokenHelper = tokenHelper;
            _userService = userService;
        }


        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            throw new NotImplementedException();
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if(userToCheck == null)
            {
                return new ErorrDataResult<User>(Messages.UserNotFound);
            }

            if(!HashingHelper.VerifyPasswordHash(userForLoginDto.Password,userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErorrDataResult<User>(Messages.PasswordError);
            }
            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte [] passwordHash,passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status= true
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IResult UserExists(string email)
        {
            if(_userService.GetByMail(email) != null)
            {
                return new ErorrResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        IDataResult<AccessToken> IAuthService.CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
    }
}
