using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
                _context = new ApplicationDbContext();
        }
        // Get /Api/Customers
        public IHttpActionResult GetCustomers() 
        {
            var customerDtos = _context.Customers.ToList().Select(Mapper.Map<Customer,CustomerDto>);
            return Ok(customerDtos);
        }

        // Get /Api/Customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
                return BadRequest();

            return Ok(Mapper.Map<Customer,CustomerDto>(customer));
        }

        // Post /Api/Customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            var customerDtos = _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
            return Ok(customerDto);
        }

        // Put /Api/customer/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDB = _context.Customers.FirstOrDefault(c => c.Id == id);

            if (customerInDB == null)
                return NotFound();

            Mapper.Map(customerDto, customerInDB);

            _context.SaveChanges();
            return Ok();
        }

        // Delete /Api/Customers/1
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDB = _context.Customers.FirstOrDefault(c => c.Id == id);

            if (customerInDB == null)
                return NotFound();

            _context.Customers.Remove(customerInDB);
            _context.SaveChanges();
            return Ok();
        }
    }
}
