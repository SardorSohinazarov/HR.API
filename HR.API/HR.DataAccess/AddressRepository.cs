using HR.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace HR.DataAccess;

public class AddressRepository:IAddressRepository
{
    private readonly AppDbContext _appDbContext;
    public AddressRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Address> CreateAddress(Address address)
    {
        await _appDbContext.Addresses.AddAsync(address);
        await _appDbContext.SaveChangesAsync();
        return address;
    }

    public async Task<bool> DeleteAddress(int id)
    {
        var address = await _appDbContext.Addresses.FindAsync(id);
        if (address is null)
            return false;
        _appDbContext.Addresses.Remove(address);
        _appDbContext.SaveChanges();
        return true;
    }

    public async Task<Address> GetAddress(int id)
    {
        return await _appDbContext.Addresses.FindAsync(id);
    }

    public async Task<IEnumerable<Address>> GetAddresses()
    {
        return await _appDbContext.Addresses.ToListAsync();
    }

    public async Task<Address> UpdateAddress(int id, Address address)
    {
        var updatedAddress = _appDbContext.Addresses.Attach(address);
        updatedAddress.State = EntityState.Modified;
        await _appDbContext.SaveChangesAsync();
        return address;
    }
}
