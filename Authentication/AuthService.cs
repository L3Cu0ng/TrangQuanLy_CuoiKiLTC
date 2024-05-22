using CuoiKiLTC.Models;
using System.Linq;

namespace CuoiKiLTC.Authentication
{
    public class AuthService
    {
        private readonly QuanLyCongTyContext _context;

        public AuthService(QuanLyCongTyContext context)
        {
            _context = context;
        }

        public Admin? Authenticate(string username, string password)
        {
            var admin = _context.Admins.SingleOrDefault(a => a.UserName == username);
            if (admin == null || !PasswordHelper.VerifyPassword(password, admin.PasswordHash))
            {
                return null; // Authentication failed
            }
            return admin; // Authentication successful
        }
    }
}
