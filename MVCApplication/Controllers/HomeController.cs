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
        public static string Searchstr;
        public static string Bridgeelement;

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

                Cube Cube = new Cube("Cube", resultViewModel.Sidelength);

                resultViewModel.Volume = Cube.Volume(resultViewModel.Sidelength);
                resultViewModel.Surfacearea = Cube.Surfacearea(resultViewModel.Sidelength);

                TheList.Add(Cube);

                resultViewModel.Shapelist = TheList;

                return View(resultViewModel);


            }


            return Redirect("/Home/Error");

        }


        [HttpGet]
        public IActionResult Remove()
        {
            if (TheList.Count > 0)
            {
                RemoveViewModel removeViewModel = new RemoveViewModel();

                removeViewModel.TheList = TheList;

                return View(removeViewModel);
            }

            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public IActionResult Remove(RemoveViewModel removeViewModel)

        {
            if (ModelState.IsValid)
            {

                TheList.RemoveAll(x => x.Name == removeViewModel.NewElement1 & x.Sidelength == removeViewModel.NewElement2);

                return Redirect("/Home/Result");
            }

            return Redirect("/Home/Error");

        }


        [HttpGet]
        public IActionResult SearchSelect()
        {
            if (TheList.Count > 0)
            {
                SearchSelectViewModel searchSelectViewModel = new SearchSelectViewModel();

                return View(searchSelectViewModel);
            }

            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public IActionResult SearchSelect(SearchSelectViewModel searchSelectViewModel)

        {
            if (ModelState.IsValid)

            {
                Searchstr = searchSelectViewModel.Searchstr.ToLower(); 
                return Redirect("/Home/SearchResult");
            }

            return Redirect("/Home/Error");

        }

        [HttpGet]
        public IActionResult SearchResult()
        {
            if (TheList.Count > 0)
            {
                SearchResultViewModel searchResultViewModel = new SearchResultViewModel();
                List<Shape> Anslist = new List<Shape>();

                foreach (Shape item in TheList)
                {
                    string itemname = item.Name.ToLower();

                    if (itemname.Contains(Searchstr))
                    {

                        Anslist.Add(item);

                    }


                }

                if (Anslist.Count == 0)
                {
                    Shape errshape = new Shape("That search returned no results", 0);

                    Anslist.Add(errshape);

                }

                ViewBag.Anslist = Anslist;

                return View(searchResultViewModel);
            }

            else
            {
                return Redirect("/");
            }

        }

        [HttpGet]
        public IActionResult Sort()
        {
            if (TheList.Count > 0)
            {
                SortViewModel sortViewModel = new SortViewModel();

                List<Shape> Bridgelist = TheList.OrderBy(x => x.Sidelength).ToList();

                sortViewModel.Sortlist = Bridgelist;

                return View(sortViewModel);
            }

            else
            {
                return Redirect("/");
            }
        }

    }

}
