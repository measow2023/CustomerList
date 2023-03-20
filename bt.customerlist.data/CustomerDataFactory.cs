namespace bt.customerlist.data;

public class CustomerDataFactory
{
    private readonly ICustomerRepository _repository;

    public CustomerDataFactory(ICustomerRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    public ICustomerRepository Create()
    {
        return _repository;
    }
}
