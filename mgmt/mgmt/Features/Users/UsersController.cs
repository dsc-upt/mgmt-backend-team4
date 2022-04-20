using mgmt.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mgmt.Features.Users;

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
    //public async Task<ActionResult<UserResponse>> Get()
    public async Task<ActionResult<UserResponse>> Get()
    {
        return Ok(
            _dbContext.Users.Select(
                user => new UserResponse
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Roles = user.Roles.ToList()
                }
            ).ToList()
        );
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
    public async Task<ActionResult<UserResponse>> Post(UserRequest userRequest)
    {
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            FirstName = userRequest.FirstName,
            LastName = userRequest.LastName,
            Email = userRequest.Email,
            Roles = userRequest.Roles
        };
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return Ok(new UserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = user.Roles
        });
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

    
    /*[HttpPut]
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

        var rolesForDelete = role.Split(",");
        var oldRoles = user.Roles.Split(",");
        string newRoles = "";
        foreach (var r in oldRoles.Except(rolesForDelete))
        {
            newRoles += (r + ",");
        }

        user.Roles = newRoles.Substring(0, newRoles.Length - 1);
        await _dbContext.SaveChangesAsync();
        return user;
    }*/
    
}