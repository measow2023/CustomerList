namespace bt.customerlist.data;

public class AddressItem : IAddressItemRecord
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
}
