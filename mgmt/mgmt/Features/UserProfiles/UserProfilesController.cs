using mgmt.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mgmt.Features.UserProfiles;

[ApiController]
[Route("api/userprofiles")]
public class UserProfilesController: ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public UserProfilesController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }   
    
    [HttpGet]
    public async Task<List<UserProfile>> Get()
    {
        return await _appDbContext.UserProfiles.Include(p => p.User).ToListAsync();
    }
}