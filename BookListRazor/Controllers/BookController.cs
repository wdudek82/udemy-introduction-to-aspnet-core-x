using System;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new {data = await _db.Book.ToListAsync()});
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteById(int id)
        {
            var book = await _db.Book.FirstOrDefaultAsync(u => u.Id == id);

            if (book == null)
            {
                return Json(new {success = false, message = "Error while deleting."});
            }

            _db.Book.Remove(book);
            await _db.SaveChangesAsync();

            Console.WriteLine($"Deleting: {id}");

            return Json(new {success = true, message = "Item has been deleted!"});
        }
    }
}
