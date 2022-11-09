using DAL.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserRepository:IUserRepository
    {
        private readonly UserContext context;
        public UserRepository(UserContext context)
        {
            this.context = context;
        }


        public Users GetUserByEmail(string email)
        {
            var data=context.users.Where(x => x.Email == email).FirstOrDefault();
            return data;
        }


        public List<Users> GetAllUser(int pageNo, int itemsPerPage, string? nameStartWith, string? nameEndWith, string? nameContains, int? ageAbove)
        {
            var query = context.users.AsQueryable();
            if (!string.IsNullOrEmpty(nameStartWith))
            {
                query = query.Where(c => c.Firstname.StartsWith(nameStartWith));
            }
            if (!string.IsNullOrEmpty(nameEndWith))
            {
                query = query.Where(c => c.Lastname.EndsWith(nameEndWith));
            }
            if(!string.IsNullOrEmpty(nameContains))
            {
                query = query.Where(c => c.Firstname.Contains(nameContains));
            }
         
            return query.Skip((pageNo-1)*itemsPerPage).Take(itemsPerPage).ToList();  
        }


        public  Users CreateUser(Users obj)
        {
            this.context.users.Add(obj);
            this.context.SaveChanges();
            return obj;
        }

        
        public Users UpdateUser(Users obj)
        {
            var data = this.context.users.Find(obj.Email);
            return data;
        }


        public Users RemoveUser(string email)
        {
            var data = this.context.users.Find(email);
            this.context.users.Remove(data);
            this.context.SaveChanges();
            return data;
        }

    }
}
