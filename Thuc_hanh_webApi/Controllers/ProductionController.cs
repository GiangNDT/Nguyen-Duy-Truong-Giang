using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Thuc_hanh_webApi.Entites;

namespace Thuc_hanh_webApi.Controllers
{
    public class ProductionController : BaseController
    {
        public ProductionController(PracticeDbContext dbContext): base(dbContext)
        { 
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var res = _dbContext.Productions;
            return Ok(res);
        }

        [HttpGet("{id:long}")]
        public IActionResult GetDetails(long id) 
        {
            return Ok(_dbContext.Productions.Find(id));
        }

        [HttpPost]
        public IActionResult Add([FromBody] Production production) 
        {
            _dbContext.Productions.Add(production);
            var eff = _dbContext.SaveChanges();
            return eff > 0 ? Ok("Success") : BadRequest("Failed");
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Production production) 
        {
            var data = _dbContext.Productions.Find(production.Id);
            if(data == null) return NotFound("Production not found");

            data.Name = production.Name;
            data.ExpDate = production.ExpDate;
            data.Status = production.Status;
            data.Price = production.Price;
            data.Amount = production.Amount;
            data.Description = production.Description;
            data.UpdatedDate = DateTime.Now;
            data.UpdatedBy = "";

            _dbContext.Productions.Update(data);
            var eff = _dbContext.SaveChanges();
            return eff> 0 ? Ok("Success") : BadRequest("Failed");

        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] long id) 
        {
            var data = _dbContext.Productions.Find(id);
            if(data == null) return NotFound("No production found");

            _dbContext.Productions.Remove(data);
            var eff = _dbContext.SaveChanges();
            return eff > 0 ? Ok("Delete success") : BadRequest("Failed");
        }
    }
}
