using E_Commerse_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerse_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        ApiContext apiContext;
        public ProductController()
        {
            apiContext = new ApiContext();
        }
        [HttpPost]
        public ActionResult PostProduct(Product product)
        {
            apiContext.products.Add(product);
            apiContext.SaveChanges();
            return NoContent();
        }
        [HttpGet]
        public List<Product> GetProducts()
        {
            return apiContext.products.ToList();
        }
        [HttpGet("GetById")]
        public ActionResult<Product> GetById(int id)
        {
            var item = apiContext.products.FirstOrDefault(e=>e.Id==id);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return item;
            }
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var item = apiContext.products.FirstOrDefault(e => e.Id == id);
            if(item == null)
            {
                return NotFound();
            }
            else
            {
                apiContext.products.Remove(item);
                apiContext.SaveChanges();
                return NoContent();
            }
        }
        [HttpPut]
        public ActionResult EditProduct(int id, Product NewProduct)
        {
            var OldProduct = apiContext.products.FirstOrDefault(e => e.Id == id);
            if (OldProduct == null)
            {
                return NotFound();
            }
            else
            {
                OldProduct.Name= NewProduct.Name;
                OldProduct.Catogry= NewProduct.Catogry;
                OldProduct.Price= NewProduct.Price;
                OldProduct.Photo= NewProduct.Photo;
                OldProduct.Enable= NewProduct.Enable;
                OldProduct.Deleted = NewProduct.Deleted;
                apiContext.SaveChanges(true);
                return NoContent();
            }
        }
    }
}
