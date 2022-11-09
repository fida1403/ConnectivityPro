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
        List<Users> GetAllUser(int pageNo, int itemsPerPage, string? nameStartWith, string? nameEndWith, string? nameContains, int? ageAbove);
        Users CreateUser(Users obj);
        Users UpdateUser(Users obj);
        Users RemoveUser(string email);
    }

    public class UserService : IUserService
    {
        private readonly UserContext _context;
        private IUserRepository repository;
        public UserService(UserContext context, IUserRepository repo)
        {
            _context = context;
            repository = repo;
        }

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
            if (result == null)
            {
                throw new Exception("The requested data is not found");
            }
            return result;
        }


        public List<Users> GetAllUser(int pageNo, int itemsPerPage, string? nameStartWith, string? nameEndWith, string? nameContains, int? ageAbove)
        {
            var result = repository.GetAllUser(pageNo, itemsPerPage, nameStartWith, nameEndWith, nameContains, ageAbove);
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

            if (obj==null || string.IsNullOrWhiteSpace(obj.Email))
            {
                throw new Exception("Invalid Request");
            }
            if (!IsValidEmail(obj.Email))
            {
                throw new Exception("Incorrect email pattern");
            }
            if (IsValidName(obj.Firstname))
            {
                throw new Exception("Name should not contain any whitespace");
            }

            var user = repository.GetUserByEmail(obj.Email);
            if (user == null)
            {
                throw new Exception("The requested data is not found");
            }
            user.Firstname = obj.Firstname;
            user.Lastname = obj.Lastname;
            user.Password=obj.Password;
            user.DOB=obj.DOB;
            user.Gender=obj.Gender;

            var result = repository.UpdateUser(obj);
            this._context.SaveChanges();
            return result;
        }


        public Users RemoveUser(string email)
        {
            var result = repository.RemoveUser(email);
            if (result == null)
            {
                throw new Exception("The requested data is not found");
            }
            return result;
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
