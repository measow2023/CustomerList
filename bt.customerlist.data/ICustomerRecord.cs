namespace bt.customerlist.data;

public interface ICustomerItemRecord
{
    int Id { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string CompanyName { get; set; }
    public IAddressItemRecord Address { get; set; }
    public string AddressBlock { get; }
}

public interface IAddressItemRecord
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
}

public interface ICustomerItem : ICustomerItemRecord
{
    bool Save();
    bool Delete();
}