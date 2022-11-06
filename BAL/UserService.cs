using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BAL
{
    public interface IUserService
    {
        Users GetUserByEmail(string email);
        List<Users> GetAllUser();
        Users CreateUser(Users obj);
        Users UpdateUser(Users obj);
        Users RemoveUser(string email);
    }

    public class UserService : IUserService
    {
        public Users GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email) == null)
            {
                throw new Exception("Eamil is not valid");
            }
            if (!IsValidEmail(email))
            {
                throw new Exception("Incorrect Email pattern");
            }
            var result = repository.GetUserByEmail(email);
            return result;
        }


        public List<Users> GetAllUser()
        {
            var result = repository.GetAllUser();
            return result;
        }


        public Users CreateUser(Users obj)
        {
            var email = obj.Email;
            if (!IsValidEmail(email))
            {
                throw new Exception("Incorrect email pattern");
            } 
            var firstname = obj.Firstname;
            if (IsValidName(firstname))
            {
                throw new Exception("Name should not contain any whitespace");
            }
            var result = repository.CreateUser(obj);
            return result;
        }


        public Users UpdateUser(Users obj)
        {
            if(obj == null || string.IsNullOrWhiteSpace(obj.Email))
            {
                throw new Exception("Invalid Request");
            }
            var email=obj.Email;
            if (!IsValidEmail(email))
            {
                throw new Exception("Incorrect email pattern");
            }
            var firstname = obj.Firstname;
            if (IsValidName(firstname))
            {
                throw new Exception("Name should not contain any whitespace");
            }
            var result=repository.UpdateUser(obj);
            return result;
        }


        public Users RemoveUser(string email)
        {
            var result = repository.RemoveUser(email);
            return result;
        }


        private IUserRepository repository;
        public UserService(IUserRepository repo)
        {
            repository = repo;
        }


        private bool IsValidName(string firstname)
        {
            return firstname.Any(x => Char.IsWhiteSpace(x));
        }


        public static bool IsValidEmail(string email)
        {
            Regex emailregex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase);
            return emailregex.IsMatch(email);
        }
    }
}
