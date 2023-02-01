using HR.API.Models;
using HR.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IGenericCRUDService<AddressModel> _addressSvc;

        public AddressController(IGenericCRUDService<AddressModel> addressSvc)
        {
            _addressSvc = addressSvc;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _addressSvc.GetAll());
        }

        [HttpGet("adress")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _addressSvc.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddressModel addressModel)
        {
            var createdAddress = await _addressSvc.Create(addressModel);
            var routeValue = new { id = createdAddress.Id };
            return CreatedAtRoute(routeValue, createdAddress);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] AddressModel addressModel)
        {
            return Ok(await _addressSvc.Update(id, addressModel));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _addressSvc.Delete(id);
            if (result)
                return NoContent();
            else
                return NotFound();
        }
    }
}
