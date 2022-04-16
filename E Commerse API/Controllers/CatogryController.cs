using E_Commerse_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerse_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatogryController : ControllerBase
    {
        ApiContext apicontext;
        public CatogryController()
        {
            apicontext = new ApiContext();

        }

        [HttpGet]
        public List<Catogry> GetCatogries()
        {
            return apicontext.Catogries.ToList();
        }

        [HttpGet("GetById")]
        public ActionResult<Catogry> GetById(int id)
        {
            var item=apicontext.Catogries.FirstOrDefault(c => c.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return item;
            }
            
        }

        [HttpPost]
        public ActionResult PostCatogry([FromBody]Catogry catogry)
        {
            apicontext.Catogries.Add(catogry);
            apicontext.SaveChanges();
            return CreatedAtAction("GetById", new { id = catogry.Id }, apicontext.Catogries.ToList());
        }
        [HttpDelete("{Id}")]
        public ActionResult DeleteCatogry(int Id)
        {
            var item = apicontext.Catogries.FirstOrDefault(e=>e.Id==Id);
            if(item == null)
            {
                return NotFound();
            }
            else
            {
                apicontext.Remove(item);
                apicontext.SaveChanges(true);
                return NoContent();
            }
        }
        [HttpPut]
        public ActionResult EditCatogry(int Id,Catogry NewCatogry) 
        {
            var OldCatogry = apicontext.Catogries.FirstOrDefault(e => e.Id == Id);
            if(OldCatogry == null)
            {
                return NotFound();
            }
            else
            {
                OldCatogry.Name = NewCatogry.Name;
                OldCatogry.Enable = NewCatogry.Enable;
                OldCatogry.Deleted = NewCatogry.Deleted;
                apicontext.SaveChanges();
                return NoContent();
            }
        }
    }
}
