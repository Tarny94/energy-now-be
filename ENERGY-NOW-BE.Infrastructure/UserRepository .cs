using ENERGY_NOW_BE.Core;
using ENERGY_NOW_BE.Core.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;




namespace ENERGY_NOW_BE.Infrastructure
{
    public class UserRepository 
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;

        public UserRepository(DataContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Method to create a new user
        public async Task<IdentityResult> CreateUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        // Method to check if a user exists by email
        public async Task<bool> UserExistsAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null; // Return true if user exists
        }
    }

}
