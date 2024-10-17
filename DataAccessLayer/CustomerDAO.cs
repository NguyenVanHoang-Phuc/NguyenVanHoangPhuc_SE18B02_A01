using BusinessOjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CustomerDAO : DbContext
    {
        private static readonly Lazy<CustomerDAO> _instance = new Lazy<CustomerDAO>(() => new CustomerDAO());
        private SqlCommand command;

        // Constructor private để ngăn việc tạo đối tượng từ bên ngoài
        private CustomerDAO() { }

        // Property để truy cập instance
        public static CustomerDAO Instance => _instance.Value;
        // Lấy tất cả khách hàng
        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            string SQL = "SELECT CustomerID, CustomerFullName, Telephone, EmailAddress, CustomerBirthday, CustomerStatus, Password FROM Customer";

            using (var command = new SqlCommand(SQL, Connection))
            {
                try
                {
                    OpenConnection();
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            customers.Add(new Customer
                            {
                                CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                                CustomerFullName = reader.IsDBNull(reader.GetOrdinal("CustomerFullName")) ? null : reader.GetString(reader.GetOrdinal("CustomerFullName")),
                                Telephone = reader.IsDBNull(reader.GetOrdinal("Telephone")) ? null : reader.GetString(reader.GetOrdinal("Telephone")),
                                EmailAddress = reader.GetString(reader.GetOrdinal("EmailAddress")),
                                CustomerBirthday = reader.IsDBNull(reader.GetOrdinal("CustomerBirthday")) ? (DateOnly?)null : DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("CustomerBirthday"))),
                                CustomerStatus = reader.IsDBNull(reader.GetOrdinal("CustomerStatus")) ? (byte?)null : reader.GetByte(reader.GetOrdinal("CustomerStatus")),
                                Password = reader.IsDBNull(reader.GetOrdinal("Password")) ? null : reader.GetString(reader.GetOrdinal("Password"))
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }

            return customers;
        }

        // Tạo một khách hàng mới
        public void CreateCustomer(Customer customer)
        {
            string SQL = "INSERT INTO Customer (CustomerFullName, Telephone, EmailAddress, CustomerBirthday, CustomerStatus, Password) VALUES (@CustomerFullName, @Telephone, @EmailAddress, @CustomerBirthday, @CustomerStatus, @Password)";

            using (var command = new SqlCommand(SQL, Connection))
            {
                command.Parameters.AddWithValue("@CustomerFullName", customer.CustomerFullName ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Telephone", customer.Telephone ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@EmailAddress", customer.EmailAddress);
                command.Parameters.AddWithValue("@CustomerBirthday", customer.CustomerBirthday.HasValue ? (object)customer.CustomerBirthday.Value.ToDateTime(new TimeOnly()) : DBNull.Value);
                command.Parameters.AddWithValue("@CustomerStatus", customer.CustomerStatus.HasValue ? (object)customer.CustomerStatus.Value : DBNull.Value);
                command.Parameters.AddWithValue("@Password", customer.Password ?? (object)DBNull.Value);

                try
                {
                    OpenConnection();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        // Cập nhật thông tin khách hàng
        public void UpdateCustomer(Customer customer)
        {
            string SQL = "UPDATE Customer SET CustomerFullName = @CustomerFullName, Telephone = @Telephone, EmailAddress = @EmailAddress, CustomerBirthday = @CustomerBirthday, CustomerStatus = @CustomerStatus, Password = @Password WHERE CustomerID = @CustomerID";

            using (var command = new SqlCommand(SQL, Connection))
            {
                command.Parameters.AddWithValue("@CustomerID", customer.CustomerId);
                command.Parameters.AddWithValue("@CustomerFullName", customer.CustomerFullName ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Telephone", customer.Telephone ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@EmailAddress", customer.EmailAddress);
                command.Parameters.AddWithValue("@CustomerBirthday", customer.CustomerBirthday.HasValue ? (object)customer.CustomerBirthday.Value.ToDateTime(new TimeOnly()) : DBNull.Value);
                command.Parameters.AddWithValue("@CustomerStatus", customer.CustomerStatus.HasValue ? (object)customer.CustomerStatus.Value : DBNull.Value);
                command.Parameters.AddWithValue("@Password", customer.Password ?? (object)DBNull.Value);

                try
                {
                    OpenConnection();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        // Xóa một khách hàng
        public void DeleteCustomer(Customer customer)
        {
            string SQL = "DELETE FROM Customer WHERE CustomerID = @CustomerID";

            using (var command = new SqlCommand(SQL, Connection))
            {
                command.Parameters.AddWithValue("@CustomerID", customer.CustomerId);

                try
                {
                    OpenConnection();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        // Lấy thông tin khách hàng theo ID
        public Customer GetCustomerById(int customerId)
        {
            Customer customer = null;
            string SQL = "SELECT CustomerID, CustomerFullName, Telephone, EmailAddress, CustomerBirthday, CustomerStatus, Password FROM Customer WHERE CustomerID = @CustomerID";

            using (var command = new SqlCommand(SQL, Connection))
            {
                command.Parameters.AddWithValue("@CustomerID", customerId);

                try
                {
                    OpenConnection();
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.Read())
                        {
                            customer = new Customer
                            {
                                CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                                CustomerFullName = reader.IsDBNull(reader.GetOrdinal("CustomerFullName")) ? null : reader.GetString(reader.GetOrdinal("CustomerFullName")),
                                Telephone = reader.IsDBNull(reader.GetOrdinal("Telephone")) ? null : reader.GetString(reader.GetOrdinal("Telephone")),
                                EmailAddress = reader.GetString(reader.GetOrdinal("EmailAddress")),
                                CustomerBirthday = reader.IsDBNull(reader.GetOrdinal("CustomerBirthday")) ? (DateOnly?)null : DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("CustomerBirthday"))),
                                CustomerStatus = reader.IsDBNull(reader.GetOrdinal("CustomerStatus")) ? (byte?)null : reader.GetByte(reader.GetOrdinal("CustomerStatus")),
                                Password = reader.IsDBNull(reader.GetOrdinal("Password")) ? null : reader.GetString(reader.GetOrdinal("Password"))
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }

            return customer;
        }

        // Lấy thông tin khách hàng theo email
        public Customer GetCustomerByEmail(string email)
        {
            Customer customer = null;
            string SQL = "SELECT CustomerID, CustomerFullName, Telephone, EmailAddress, CustomerBirthday, CustomerStatus, Password FROM Customer WHERE EmailAddress = @EmailAddress";

            using (var command = new SqlCommand(SQL, Connection))
            {
                command.Parameters.AddWithValue("@EmailAddress", email);

                try
                {
                    OpenConnection();
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.Read())
                        {
                            customer = new Customer
                            {
                                CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                                CustomerFullName = reader.IsDBNull(reader.GetOrdinal("CustomerFullName")) ? null : reader.GetString(reader.GetOrdinal("CustomerFullName")),
                                Telephone = reader.IsDBNull(reader.GetOrdinal("Telephone")) ? null : reader.GetString(reader.GetOrdinal("Telephone")),
                                EmailAddress = reader.GetString(reader.GetOrdinal("EmailAddress")),
                                CustomerBirthday = reader.IsDBNull(reader.GetOrdinal("CustomerBirthday")) ? (DateOnly?)null : DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("CustomerBirthday"))),
                                CustomerStatus = reader.IsDBNull(reader.GetOrdinal("CustomerStatus")) ? (byte?)null : reader.GetByte(reader.GetOrdinal("CustomerStatus")),
                                Password = reader.IsDBNull(reader.GetOrdinal("Password")) ? null : reader.GetString(reader.GetOrdinal("Password"))
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }

            return customer;
        }

    }
}
