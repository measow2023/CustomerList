using Microsoft.AspNetCore.Mvc;
using bt.customerlist.data;
using bt.customerlist.web.ViewModel;

namespace bt.customerlist.web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerListController : ControllerBase
{
    private readonly CustomerDataFactory _dataFactory;

    public CustomerListController(CustomerDataFactory dataFactory)
    {
        _dataFactory = dataFactory ?? throw new ArgumentNullException(nameof(dataFactory));
    }
    [HttpGet(Name = "Get All Customers")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ICustomerItem>))]
    public ActionResult<IEnumerable<ICustomerItem>> GetAll()
    {
        ICustomerRepository customerRepo = _dataFactory.Create();
        return Ok(customerRepo.GetList());
    }

    [HttpGet("{customerId}", Name = "Get By Id")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ICustomerItem>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ICustomerItem> GetById(int customerId)
    {
        ICustomerRepository customerRepo = _dataFactory.Create();
        CustomerItem customer = (CustomerItem) customerRepo.GetCustomerById(customerId);
        if (customer is null)
        {
            return NotFound();
        }
        return Ok(customer);
    }

    [HttpPost(Name = "Create Customer")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult Create(CustomerRequest request)
    {
        ICustomerRepository customerRepo = _dataFactory.Create();
        var customerItem = (CustomerItem) customerRepo.GetNewCustomer();
        customerItem.FirstName = request.FirstName;
        customerItem.LastName = request.LastName;
        customerItem.CompanyName = request.CompanyName;
        customerItem.Address = new AddressItem
        {
            Street = request.Address.Street,
            City = request.Address.City,
            State = request.Address.State,
            Zip = request.Address.Zip
        };

        customerItem.Save();
        return Created(nameof(GetById), new { id = customerItem.Id });

    }

    [HttpDelete("{id}", Name = "Delete Customer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete(int id)
    {
        ICustomerRepository customerRepo = _dataFactory.Create();
        CustomerItem customer = (CustomerItem)customerRepo.GetCustomerById(id);
        if (customer is null)
        {
            return NotFound();
        }
        customer.Delete();
        return NoContent();
    }
}