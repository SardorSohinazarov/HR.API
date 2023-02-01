using HR.DataAccess.Entities;

namespace HR.DataAccess;

public interface IAddressRepository
{
    public Task<IEnumerable<Address>> GetAddresses();
    public Task<Address> GetAddress(int id);
    public Task<Address> CreateAddress(Address address);
    public Task<Address> UpdateAddress(int id, Address address);
    public Task<bool> DeleteAddress(int id);
}
