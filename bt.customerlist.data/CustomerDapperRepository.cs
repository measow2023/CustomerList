using Dapper;
using Microsoft.Data.Sqlite;

namespace bt.customerlist.data;

public class CustomerDapperRepository : ICustomerRepository
{
    private readonly DatabaseConfig _databaseConfig;
    public CustomerDapperRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig ?? throw new ArgumentNullException(nameof(databaseConfig));
    }

    public ICustomerItem GetCustomerById(int id)
    {
        var query = @"SELECT Id, 
                        FirstName, 
                        LastName, 
                        CompanyName, 
                        Street, 
                        City, 
                        State, 
                        Zip 
                        FROM Customer 
                        WHERE Id = @id";


        using var connection = new SqliteConnection(_databaseConfig.Name);
        CustomerDto customerDto = connection.Query<CustomerDto>(query, new { id }).FirstOrDefault();
        return new CustomerItem(customerDto, this);
        
    }

    public IEnumerable<ICustomerItem> GetList()
    {
        List<CustomerItem> result = new List<CustomerItem>();
        using var connection = new SqliteConnection(_databaseConfig.Name);        
        IEnumerable<CustomerDto> customDtos = connection.Query<CustomerDto>(@"SELECT Id, FirstName, LastName, CompanyName, Street, City, State, Zip FROM Customer").ToList();
        result = customDtos.Select(x => new CustomerItem(x, this)).ToList();
        return result;
        
    }

    public ICustomerItem GetNewCustomer()
    {
        return new CustomerItem(new CustomerDto(), this);
    }

    public bool DeleteCustomer(int id)
    {
        var query = @"DELETE FROM Customer WHERE Id = @id";
        using var connection = new SqliteConnection(_databaseConfig.Name);
        int rowsAffected = connection.Execute(query, new { id });
        return rowsAffected > 0;
        
    }

    public bool Save(CustomerDto customerDto)
    {
        var query = @"INSERT INTO Customer 
                    (Id, 
                        FirstName, 
                        LastName, 
                        CompanyName, 
                        Street, 
                        City, 
                        State, 
                        Zip) 
                    VALUES(@Id, 
                        @FirstName, 
                        @LastName, 
                        @CompanyName, 
                        @Street, 
                        @City, 
                        @State, 
                        @Zip);";
        using var connection = new SqliteConnection(_databaseConfig.Name);        
        int nextId = GetNextCustomerId();
        customerDto.Id = nextId;
        int rowsAffected = connection.Execute(query, customerDto);
        return rowsAffected > 0;
        
    }

    private int GetNextCustomerId()
    {
        var query = @"SELECT CAST((MAX(Id) + 1) AS INT)  FROM Customer";
        using var connection = new SqliteConnection(_databaseConfig.Name);        
        var nextCustomerId = (long) connection.ExecuteScalar(query);
        return Convert.ToInt32(nextCustomerId);        
    }
}
