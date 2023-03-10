using HR.API.Models;
using HR.DataAccess.Entities;
using HR.DataAccess.Repository;

namespace HR.API.Services;

public class AddressCRUDService: IGenericCRUDService<AddressModel>
{
    private readonly IGenericRepository<Address> _addressRepository;
    public AddressCRUDService(IGenericRepository<Address> addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<AddressModel> Create(AddressModel addressModel)
    {
        var address = new Address
        {
            Id = addressModel.Id,
            AddressLine1 = addressModel.AddressLine1,
            AddressLine2 = addressModel.AddressLine2,
            PostalCode = addressModel.PostalCode,
            Country = addressModel.Country,
            City = addressModel.City,
        };
        var createdAddress = await _addressRepository.Create(address);
        var createAddressModel = new AddressModel
        {
            Id = createdAddress.Id,
            AddressLine1= createdAddress.AddressLine1,
            AddressLine2 = createdAddress.AddressLine2,
            PostalCode = createdAddress.PostalCode,
            City = createdAddress.City,
            Country = createdAddress.Country,
            
        };
        return createAddressModel;
    }

    public async Task<bool> Delete(int id)
    {
        return await _addressRepository.Delete(id);
    }

    public async Task<AddressModel> Get(int id)
    {
        var adress = await _addressRepository.Get(id);
        var model = new AddressModel
        {
            Id = adress.Id,
            AddressLine1 = adress.AddressLine1,
            AddressLine2 = adress.AddressLine2,
            PostalCode = adress.PostalCode,
            City = adress.City,
            Country = adress.Country,
        };
        return model;
    }

    public async Task<IEnumerable<AddressModel>> GetAll()
    {
        var employees = await _addressRepository.GetAll();
        var result = new List<AddressModel>();
        foreach (var employee in employees)
        {
            var addressModel = new AddressModel
            {
                Id = employee.Id,
                AddressLine1 = employee.AddressLine1,
                AddressLine2 = employee.AddressLine2,
                PostalCode = employee.PostalCode,
                City = employee.City,
                Country = employee.Country,
            };
            result.Add(addressModel);
        }
        return result;
    }

    public async Task<AddressModel> Update(int id, AddressModel addressModel)
    {
        var address = new Address
        {
            Id = addressModel.Id,
            AddressLine1 = addressModel.AddressLine1,
            AddressLine2 = addressModel.AddressLine2,
            PostalCode = addressModel.PostalCode,
            City = addressModel.City,
            Country = addressModel.Country,
        };
        var updatedAddres = await _addressRepository.Update(id, address);
        var updatedAddressModel = new AddressModel
        {
            Id = updatedAddres.Id,
            AddressLine1 = updatedAddres.AddressLine1,
            AddressLine2 = updatedAddres.AddressLine2,
            PostalCode = updatedAddres.PostalCode,
            City = updatedAddres.City,
            Country = updatedAddres.Country,
        };
        return updatedAddressModel;
    }
}
