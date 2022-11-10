namespace DAL.Models
{
    public interface IUserRepository
        {
            public Users CreateUser(Users obj);
            public Users? GetUserByEmail(string email);
            public List<Users> GetAllUser(int pageNo, int itemsPerPage, string? nameStartWith, string? nameEndWith, string? nameContains, int? ageAbove, int? ageBelow, int? ageExact);
            public Users UpdateUser(Users obj);
            public Users RemoveUser(string email);

        }
}
