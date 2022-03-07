using mgmt.Database;
using mgmt.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mgmt.Teams;

[ApiController]
[Route("api/teams")]
public class TeamsController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public TeamsController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet]
    public async Task<List<Team>> Get()
    {
        return await _appDbContext.Teams.Include(u => u.TeamLead).ToListAsync();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<Team> GetById(string id)
    {
        var team = await _appDbContext.Teams.Include(u => u.TeamLead).FirstOrDefaultAsync(team => team.Id == id);
        if (team is null)
        {
            throw new ArgumentException("User not found!");
        }

        return team;
    }

    [HttpPost]
    public async Task<Team> Post(TeamRequest entity)
    {
        var team = new Team()
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            TeamLead = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Id == entity.TeamLead),
            Name = entity.Name,
            GithubLink = entity.GithubLink
        };
        if (team.TeamLead is null)
        {
            throw new ArgumentException("Team Lead not found!");
        }

        var result = await _appDbContext.Teams.AddAsync(team);
        await _appDbContext.SaveChangesAsync();
        return result.Entity;
    }

    [HttpDelete]
    public async Task<Team> Delete(string id)
    {
        var team = await _appDbContext.Teams.FirstOrDefaultAsync(team => team.Id == id);
        if (team is null)
        {
            throw new ArgumentException("Team not found!");
        }

        _appDbContext.Teams.Remove(team);
        await _appDbContext.SaveChangesAsync();
        return team;
    }

    [HttpPut]
    public async Task<Team> Update(TeamRequest entity, string id)
    {
        var team = await _appDbContext.Teams.FirstOrDefaultAsync(t => t.Id == id);
        if (team is null)
        {
            throw new ArgumentException("Team not found!");
        }

        team.TeamLead = await _appDbContext.Users.FirstOrDefaultAsync(tl => tl.Id == entity.TeamLead);
        team.Name = entity.Name;
        team.GithubLink = entity.GithubLink;
        if (team.TeamLead is null)
        {
            throw new ArgumentException("Team Lead not found!");
        }

        await _appDbContext.SaveChangesAsync();
        return team;
    }

    [HttpPut]
    [Route("/api/teams/{id}/teamlead/{idTeamLead}")]
    public async Task<Team> changeTeamLead(string id, string idTeamLead)
    {
        var team = await _appDbContext.Teams.FirstOrDefaultAsync(t => t.Id == id);
        if (team is null)
        {
            throw new ArgumentException("Team not found!");
        }

        var tmld = await _appDbContext.Users.FirstOrDefaultAsync(tl => tl.Id == idTeamLead);
        if (tmld is null)
        {
            throw new ArgumentException("Team Lead not found!");
        }

        team.TeamLead = tmld;
        await _appDbContext.SaveChangesAsync();
        return team;
    }
}

