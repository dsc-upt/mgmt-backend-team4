using mgmt.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mgmt.Features.Clients;

[ApiController]
[Route("api/clients")]
public class ClientsController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public ClientsController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet]
    public async Task<List<Client>> Get()
    {
        return await _appDbContext.Clients.Include(c => c.ContactPerson).ToListAsync();
    }
    [HttpPost]
    public async Task<Client> Post(ClientRequest entity)
    {
        var client = new Client()
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            ContactPerson = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Id == entity.ContactPerson),
            Name = entity.Name,
            Email = entity.Email,
            Phone = entity.Phone
        };
        if (client.ContactPerson is null)
        {
            throw new ArgumentException("Contact Person not found!");
        }

        var result = await _appDbContext.Clients.AddAsync(client);
        await _appDbContext.SaveChangesAsync();
        return result.Entity;
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<Client> GetById(string id)
    {
        var client = await _appDbContext.Clients.Include(c => c.ContactPerson).FirstOrDefaultAsync(cl => cl.Id == id);
        if (client is null)
        {
            throw new ArgumentException("Client not found!");
        }

        return client;
    }
    
    [HttpDelete]
    public async Task<Client> Delete(string id)
    {
        var client = await _appDbContext.Clients.FirstOrDefaultAsync(Cl => Cl.Id == id);
        if (client is null)
        {
            throw new ArgumentException("Client not found!");
        }

        _appDbContext.Clients.Remove(client);
        await _appDbContext.SaveChangesAsync();
        return client;
    }
    
    [HttpPut]
    [Route("{id}")]
    public async Task<Client> Update(ClientRequest entity, string id)
    {
        var client = await _appDbContext.Clients.FirstOrDefaultAsync(c => c.Id == id);
        if (client is null)
        {
            throw new ArgumentException("Client not found!");
        }

        client.ContactPerson = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == entity.ContactPerson);
        client.Name = entity.Name;
        client.Email = entity.Email;
        client.Phone = entity.Phone;
        if (client.ContactPerson is null)
        {
            throw new ArgumentException("Contact Person not found!");
        }

        await _appDbContext.SaveChangesAsync();
        return client;
    }

    [HttpPut]
    [Route("{id}/contactperson/{idContactPerson}")]
    public async Task<Client> changeContactPerson(string id, string idContactPerson)
    {
        var client = await _appDbContext.Clients.FirstOrDefaultAsync(c => c.Id == id);
        if (client is null)
        {
            throw new ArgumentException("Client not found!");
        }
        client.ContactPerson = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == idContactPerson);
        if (client.ContactPerson is null)
        {
            throw new ArgumentException("Contact Person not found!");
        }

        await _appDbContext.SaveChangesAsync();
        return client;
    }
}