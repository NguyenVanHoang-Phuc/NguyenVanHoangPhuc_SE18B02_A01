using BusinessOjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public List<Customer> GetAllCustomers() => CustomerDAO.Instance.GetAllCustomers();
        public void CreateCustomer(Customer customer) => CustomerDAO.Instance.CreateCustomer(customer);
        public void UpdateCustomer(Customer customer) => CustomerDAO.Instance.UpdateCustomer(customer);
        public void DeleteCustomer(Customer customer) => CustomerDAO.Instance.DeleteCustomer(customer);
        public Customer GetCustomerById(int id) => CustomerDAO.Instance.GetCustomerById(id);
        public Customer GetCustomerByEmail(string email) => CustomerDAO.Instance.GetCustomerByEmail(email);
    }
}
