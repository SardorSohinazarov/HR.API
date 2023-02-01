using HR.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace HR.DataAccess.Repository;

public class AddressRepository : IGenericRepository<Address>
{
    private readonly AppDbContext _appDbContext;
    public AddressRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Address> Create(Address address)
    {
        await _appDbContext.Addresses.AddAsync(address);
        await _appDbContext.SaveChangesAsync();
        return address;
    }

    public async Task<bool> Delete(int id)
    {
        var address = await _appDbContext.Addresses.FindAsync(id);
        if (address is null)
            return false;
        _appDbContext.Addresses.Remove(address);
        _appDbContext.SaveChanges();
        return true;
    }

    public async Task<Address> Get(int id)
    {
        return await _appDbContext.Addresses.FindAsync(id);
    }

    public async Task<IEnumerable<Address>> GetAll()
    {
        return await _appDbContext.Addresses.ToListAsync();
    }

    public async Task<Address> Update(int id, Address address)
    {
        var updatedAddress = _appDbContext.Addresses.Attach(address);
        updatedAddress.State = EntityState.Modified;
        await _appDbContext.SaveChangesAsync();
        return address;
    }
}
