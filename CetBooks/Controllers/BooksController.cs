using CetBooks.Models;
using Microsoft.AspNetCore.Mvc;

namespace CetBooks.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            FakeDB fakeDB = new FakeDB();
            var allbooks = fakeDB.GetAllBooks();

            return View(allbooks);
        }

        public IActionResult Detail(int? id) //int? nullable bir tamsayıdır
        {
            if (!id.HasValue) return BadRequest(); //HasValue, nullable bir türün
                                                   //değerinin null olup olmadığını
                                                   //kontrol etmek için kullanılan bir özelliktir.

            FakeDB db = new FakeDB();
            var book = db.GetBookById(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);

        }
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();

            FakeDB db = new FakeDB();
            var result = db.DeleteBook(id.Value);
            if (result)  //kitap başarıyla silinirse
            {

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            FakeDB db = new FakeDB();
            var result = db.CreateBook(book);
            if (result)  //kitap başarıyla eklenirse
            {
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }


    }
}
