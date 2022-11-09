using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
        public interface IUserRepository
        {
            public Users CreateUser(Users obj);
            public Users? GetUserByEmail(string email);
            public List<Users> GetAllUser(int pageNo, int itemsPerPage, string? nameStartWith, string? nameEndWith, string? nameContains, int? ageAbove);
            public Users UpdateUser(Users obj);
            public Users RemoveUser(string email);

        }
}
