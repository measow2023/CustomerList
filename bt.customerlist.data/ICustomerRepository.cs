namespace bt.customerlist.data
{
    public interface ICustomerRepository
    {
        IEnumerable<ICustomerItem> GetList();
        ICustomerItem GetNewCustomer();
        ICustomerItem GetCustomerById(int id);
        bool DeleteCustomer(int id);
        bool Save(CustomerDto customer);
    }
}