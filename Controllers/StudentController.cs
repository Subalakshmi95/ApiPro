using ApiPro.Data;
using ApiPro.Models;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPro.Controllers
{
    [Route("Home/[Controller]")]
    public class StudentController : Controller
    {
        private readonly AppDbcontext _appDbcontext;
        public StudentController(AppDbcontext appDbcontext)
        {
            _appDbcontext = appDbcontext;   
        }
        [HttpGet]
        public ActionResult <IEnumerable<Customer>> Get()
        {
            var data=_appDbcontext.Customers.ToList();
            return data;
        }
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            _appDbcontext.Customers.Add(customer);
            _appDbcontext.SaveChanges();
            return CreatedAtAction("Get", new{Id=customer.Id},customer);
        }
        [HttpPut("{Id}")]
        public IActionResult Edit(int Id, Customer customer)
        {
            if (Id != customer.Id)
            {
                return BadRequest();
            }
            _appDbcontext.Entry(customer).State = EntityState.Modified;
            _appDbcontext.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var data = _appDbcontext.Customers.Find(Id);
            if (data == null)
            {
                return NotFound();
            }
            _appDbcontext.Customers.Remove(data);
            _appDbcontext.SaveChanges();
            return NoContent();
        }
        [HttpPatch("{Id}")]
        public IActionResult Patch(int Id, JsonPatchDocument<Customer> json)
        {
            if (json == null)
            {
                return BadRequest();
            }

            // Find the customer record in the database
            var data = _appDbcontext.Customers.Find(Id);
            if (data == null)
            {
                return NotFound();
            }

            // Apply the patch document to the customer
            json.ApplyTo(data, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

            // Check if the model state is valid after applying the patch
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mark the entity as modified and save changes to the database
            _appDbcontext.Entry(data).State = EntityState.Modified;
            _appDbcontext.SaveChanges();

            return NoContent();
        }


    }
}
