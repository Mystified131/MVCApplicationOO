using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCApplication.Models;
using MVCApplication.ViewModels;

namespace MVCApplication.Controllers
{

    public class HomeController : Controller
    {
        public static List<Shape> TheList = new List<Shape>();

        public IActionResult Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel();

            return View(indexViewModel);
        }

        public IActionResult Error()
        {

            return View();
        }

        public IActionResult Result()
        {
            ResultViewModel resultViewModel = new ResultViewModel();

            resultViewModel.Error = "To add a new shape, please return to the 'Add' page.";

            resultViewModel.Shapelist = TheList;

            return View(resultViewModel);
        }

        [HttpPost]
        public IActionResult Result(ResultViewModel resultViewModel)

        {
            if (ModelState.IsValid)
            {
                if(resultViewModel.Shapetype == "Cube") { 

                Cube Cube = new Cube("Cube", resultViewModel.Sidelength);

                resultViewModel.Volume = Cube.Volume(resultViewModel.Sidelength);
                resultViewModel.Surfacearea = Cube.Surfacearea(resultViewModel.Sidelength);

                TheList.Add(Cube);

                }

                if (resultViewModel.Shapetype == "Square")
                {

                    Square Square = new Square("Square", resultViewModel.Sidelength);

                    resultViewModel.Perimeter = Square.Perimeter(resultViewModel.Sidelength);
                    resultViewModel.Area = Square.Area(resultViewModel.Sidelength);

                    TheList.Add(Square);

                }

                if (resultViewModel.Shapetype == "Segment")
                {

                    Segment Segment = new Segment("Segment", resultViewModel.Sidelength);

                    TheList.Add(Segment);

                }

                resultViewModel.Shapelist = TheList;

                return View(resultViewModel);


            }


            return Redirect("/Home/Error");

        }

    }

}
