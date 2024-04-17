using FluentValidation;
using HotelBooking2.Infrastructure;
using HotelBooking2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HotelBooking2.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly HotelBookingDbContext _context;
        private readonly AbstractValidator<CreateCustomerDTO> _customerValidator;

        public CustomerRepository(HotelBookingDbContext context, AbstractValidator<CreateCustomerDTO> customerValidator)
        {
            _context = context;
            _customerValidator = customerValidator;
        }

        public async Task<CreateCustomerDTO> CreateCustomer(CreateCustomerDTO customer)
        {
            var validationResult = await _customerValidator.ValidateAsync(customer);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            Customer customerCreate = new Customer
            {
                CustomerID = Guid.NewGuid(),
                Email = customer.Email,
                Password = customer.Password,
                Name = customer.Name,
                Birthday = customer.Birthday
            };

            _context.Customers.Add(customerCreate);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> GetCustomerByEmailAndPassword(string email, string password)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Email == email && c.Password == password);
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task UpdateCustomer(Customer existingCustomer, UpdateCustomerDTO updatedCustomerDto)
        {
            if (!string.IsNullOrEmpty(updatedCustomerDto.Name))
            {
                existingCustomer.Name = updatedCustomerDto.Name;
            }
            if (!string.IsNullOrEmpty(updatedCustomerDto.Email))
            {
                existingCustomer.Email = updatedCustomerDto.Email;
            }
            if (!string.IsNullOrEmpty(updatedCustomerDto.Password))
            {
                existingCustomer.Password = updatedCustomerDto.Password;
            }
            if (updatedCustomerDto.Birthday != default(DateTime))
            {
                existingCustomer.Birthday = updatedCustomerDto.Birthday;
            }

            await _context.SaveChangesAsync();
        }


        public async Task DeleteCustomer(string email, string password)
        {
            var customer = await GetCustomerByEmailAndPassword(email, password);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
