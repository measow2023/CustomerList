namespace bt.customerlist.data;

public class CustomerItem : ICustomerItem
{
    private readonly ICustomerRepository _repository;
    private readonly CustomerDto _record;
    internal CustomerItem(CustomerDto record, ICustomerRepository repository)
    {
        _record = record ?? throw new ArgumentNullException(nameof(record));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public int Id
    {
        get { return _record.Id; }
        set { _record.Id = value; }
    }

    public string FirstName
    {
        get { return _record.FirstName; }
        set { _record.FirstName = value; }
    }

    public string LastName
    {
        get { return _record.LastName; }
        set { _record.LastName = value; }
    }

    public string CompanyName
    {
        get { return _record.CompanyName; }
        set { _record.CompanyName = value; }
    }
    public IAddressItemRecord Address
    {
        get
        {
            return new AddressItem
            {
                Street = _record.Street,
                City = _record.City,
                State = _record.State,
                Zip = _record.Zip
            };
        }
        set
        {
            _record.Street = value.Street;
            _record.City = value.City;
            _record.State = value.State;
            _record.Zip = value.Zip;
        }
    }

    public string AddressBlock
    {
        get
        {
            return $"{_record.Street}  {Environment.NewLine} {_record.City}, {_record.State}  {_record.Zip}";
        }
    }

    public bool Delete()
    {
        return _repository.DeleteCustomer(Id);
    }

    public bool Save()
    {
        return _repository.Save(_record);
    }
}

