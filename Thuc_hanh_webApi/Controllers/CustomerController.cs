using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks.Dataflow;
using Thuc_hanh_webApi.Entites;

namespace Thuc_hanh_webApi.Controllers
{
    public class CustomerController : BaseController
    {
        public CustomerController(PracticeDbContext dbContext)
        : base(dbContext)
        {
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var res = _dbContext.Customers.Select(m => new CustomerModel 
            {
                Address = m.Address,
                Age = m.Age,
                Description = m.Description,
                Gender = m.Gender,
                Id = m.Id,
                Name = m.Name,
                Status = m.Status,
                Username = m.Username
            });
            return Ok(res);
        }

        [HttpGet("{id:long}")]
        public IActionResult GetDetails(long id)
        {
            var cus = _dbContext.Customers.Find(id);
            if(cus == null) return NotFound("Customer not found");

            var order = from co in _dbContext.Orders
                        join po in _dbContext.Productions on co.ProductId equals po.Id
                        where co.CustomerId == id
                        select new Production
                        {
                            Id = po.Id,
                            Name = po.Name,
                            Description = po.Description,
                            Amount = po.Amount,
                            Status = po.Status
                        };
            ;
            return Ok(new CustomerDetailModel 
            {
                Id = cus.Id,
                Name = cus.Name,
                Age = cus.Age,
                Address = cus.Address,
                Gender = cus.Gender,
                Status = cus.Status,
                Username = cus.Username,
                Description = cus.Description,
                Productions = order.ToList()
            });
        }

        [HttpPost]
        public IActionResult Add([FromBody] CustomerModel m)
        {
            var data = new Customer
            {
                Address = m.Address,
                Age = m.Age,
                Description = m.Description,
                Gender = m.Gender,
                Id = m.Id,
                Name = m.Name,
                Status = m.Status,
                Username = m.Username,
                CreatedDate = DateTime.Now,
                CreatedBy = ""
            };
            _dbContext.Customers.Add(data);
            var eff = _dbContext.SaveChanges();
            return eff > 0 ? Ok("Success") : BadRequest("Failed");
        }

        [HttpPut]
        public IActionResult Edit([FromBody] CustomerModel customer)
        {
            var data = _dbContext.Customers.Find(customer.Id);
            if (data == null) return NotFound("Customer not found");

            data.Name = customer.Name;
            data.Age = customer.Age;
            data.Gender = customer.Gender;
            data.Address = customer.Address;
            /*data.Username = customer.Username;
            data.Password = customer.Password;*/
            data.Status = customer.Status;
            data.Description = customer.Description;
            data.UpdatedDate = DateTime.Now;
            data.UpdatedBy = "";

            _dbContext.Customers.Update(data);
            var eff = _dbContext.SaveChanges();
            return eff > 0 ? Ok("Success") : BadRequest("Failed");

        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] long id)
        {
            var data = _dbContext.Customers.Find(id);
            if (data == null) return NotFound("No customer found");

            _dbContext.Customers.Remove(data);
            var eff = _dbContext.SaveChanges();
            return eff > 0 ? Ok("Delete success") : BadRequest("Failed");
        }

        [HttpPost]
        public IActionResult AddProductionsToOrder(Order model) 
        {
            var dataCustomer = _dbContext.Customers.Find(model.CustomerId);
            if (dataCustomer == null) return NotFound("No customer found");

            var dataProduction = _dbContext.Productions.Find(model.ProductId);
            if (dataProduction == null) return NotFound("No production found");

            var data = _dbContext.Orders.Find(model.CustomerId, model.ProductId);
            if (data != null) return NotFound("Data Exists");
            
            var produntion = new Order
            {
                CustomerId = model.CustomerId,
                ProductId = model.ProductId,
                //Amount = model.Amount,
                CreatedDate = DateTime.Now,
                CreatedBy = ""
            };
            _dbContext.Orders.Add(produntion);
            var eff = _dbContext.SaveChanges();
            return eff > 0 ? Ok("Success") : BadRequest("Failed");
        }
    }
}
