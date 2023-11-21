using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DTOs;
using Shared.Models;

namespace EfcDataAccess.DAOs;

public class UserEfcDao : IUserDao
{
    private readonly ForumContext context;

    public UserEfcDao(ForumContext context)
    {
        this.context = context;
    }
    
    public async Task<User> RegisterAsync(User user)
    {
        EntityEntry<User> newUser = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        User? user = await context.Users.FirstOrDefaultAsync(user => user.Username.Equals(username));
        return user;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        User? user = await context.Users.FindAsync(id);
        return user;
    }

    public async Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        IQueryable<User> query = context.Users.AsQueryable();
        
        if (!string.IsNullOrEmpty(searchParameters.UsernameContains))
        {
            query = query.Where(user => user.Username.Equals(searchParameters.UsernameContains));
        }

        List<User> users = await query.ToListAsync();
        return users;
    }
}