using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplicationDotNet.Data;
using WebApplicationDotNet.Models;

namespace WebApplicationDotNet.Controllers
{
    [Authorize(Roles = "user")]
    public class CarController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CarController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cars = _context.Cars.Where(c => c.UserId == userId).ToList();

            return View(cars);
        }
        [HttpGet]
        public IActionResult CreateCar() 
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var car = new Car
            {
                UserId = userId  
            };
            return View(car);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCar([Bind("BrandAndModel,ManufactureDate,IsAvailable,RentalPrice,UserId")] Car car) 
        {
            if (!ModelState.IsValid)
            {
                return View(car);
            }
            try
            {
                await _context.Cars.AddAsync(car);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index)); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View(car); 
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditCar(int id)
        {
            var existingCar = await _context.Cars.FindAsync(id);
            if (existingCar == null) return View("NotFound");
            return View(existingCar);
        }
        [HttpPost]
        public async Task<IActionResult> EditCar(int id, [Bind("BrandAndModel,ManufactureDate,IsAvailable,RentalPrice,UserId")] Car car)
        {
            if (!ModelState.IsValid) return View(car);
            var existingCar = await _context.Cars.FindAsync(id);
            if (existingCar == null) return View("NotFound");
            try
            {
                existingCar.BrandAndModel = car.BrandAndModel;
                existingCar.ManufactureDate = car.ManufactureDate;
                existingCar.IsAvailable = car.IsAvailable;
                existingCar.RentalPrice = car.RentalPrice;
                existingCar.UserId = car.UserId;

                _context.Update(existingCar);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View(car);
            }
        }

        public async Task<IActionResult> DeleteCar(int id)
        {
            var existingCar = await _context.Cars.FindAsync(id);
            if (existingCar == null) return View("NotFound");

            try
            {
                _context.Cars.Remove(existingCar);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }
    }
}
