using mgmt.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mgmt.Users;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    // Dependency Injection
    public UsersController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet]
    public async Task<List<User>> Get()
    {
        return await _dbContext.Users.ToListAsync();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<User>GetById(string id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        if (user is null)
        {
            throw new ArgumentException("User not found!");
        }
        return user;
    }

    [HttpPost]
    public async Task<User> Post(UserRequest entity)
    {
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Roles = entity.Roles,
            Email = entity.Email
        };

        var result = await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
    }
    
    [HttpDelete]
    public async Task<User> Delete(string id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id.ToString());
        if (user is null)
        {
            throw new ArgumentException("User not found!");
        }
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }
    
    [HttpPut]
    public async Task<User> Update(UserRequest User, string id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        if (user is null)
        {
            throw new ArgumentException("User not found!");
        }
        user.Email = User.Email;
        user.FirstName = User.FirstName;
        user.LastName = User.LastName;
        user.Roles = User.Roles;
        user.Updated = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
        return user;
    }

    [HttpPut]
    [Route("/api/users/role/{id}")]
    public async Task<User> AddRole(string id, string role)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        if (user is null)
        {
            throw new ArgumentException("User not found!");
        }

        var roles = role.Split(",");
        foreach (var r in roles)
        {
            if (!user.Roles.Contains(r))
                user.Roles += ("," + r);
        }
           
        await _dbContext.SaveChangesAsync();
        return user;
    }
    
    [HttpDelete]
    [Route("/api/users/role/{id}")]
    public async Task<User> DeleteRole(string id, string role)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        if (user is null)
        {
            throw new ArgumentException("User not found!");
        }

        var roles = role.Split(",");//for delete
        var oldRoles = user.Roles.Split(",");
        string newRoles = "";
        foreach (var r in oldRoles.Except(roles))
        {
            newRoles += (r + ",");
        }

        user.Roles = newRoles;
        user.Roles = user.Roles.Substring(0, user.Roles.Length - 1);
        await _dbContext.SaveChangesAsync();
        return user;
    }
    
}