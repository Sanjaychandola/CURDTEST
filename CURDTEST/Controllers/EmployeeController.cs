using CURDTEST.Models;
using CURDTEST.NewFolder;
using Microsoft.AspNetCore.Mvc;

namespace CURDTEST.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDal Dal;

        public EmployeeController(EmployeeDal dal)
        {
            Dal = dal;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            try
            {


                employees = Dal.GetAll();
            }

            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View(employees);

        }

        public IActionResult Add()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Add(Employee model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                      TempData["successMessage"] = "Employee details saved successfully!"; 
                }
                bool result = Dal.Insert(model);
                if (!result)
                {
                    TempData["errorMessage"] = "Unable to save the data!";
                    return View();

                }
                TempData["successMessage"] = "Employee details saved successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error Message"] = ex.Message;
                return View();

            }






        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            try
            {
                Employee employee = Dal.GetById(Id);
                if (employee.Id == 0)
                {
                    TempData["errorMessage"] = $"Employeee details not found with the Id: {Id}";
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }


        }
        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Model data is invalid!";
                    return View();
                }
                bool result = Dal.Update(model);
                if (!result)
                {
                    TempData["errorMessage"] = "Unable to update the data!";
                    return View();

                }
                TempData["successMessage"] = "Employee details updated successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();

            }




        }








        [HttpGet]
        public IActionResult Delete(int Id)
        {
            try
            {
                Employee employee = Dal.GetById(Id);
                if (employee.Id == 0)
                {
                    TempData["errorMessage"] = $"Employeee details not found with the Id: {Id}";
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }


        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Employee model)
        {
            try
            {
                bool result = Dal.Delete(model.Id);
                if (!result)
                {
                    TempData["errorMessage"] = "Unable to delete the data!";
                    return View();

                }
                TempData["successMessage"] = "Employee details deleted successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();

            }




        }
    }
}