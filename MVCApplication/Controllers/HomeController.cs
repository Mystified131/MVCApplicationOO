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
        public static List<Shape> Remlist = new List<Shape>();
        public static string Searchstr;
        public static string Bridgeelement;
        public static string remname;

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

                foreach(Shape item in TheList)
                {
                    if(item.Name == removeViewModel.NewElement1)
                    {

                        Remlist.Add(item);

                    }
          

                }

                remname = removeViewModel.NewElement1;

                return Redirect("/Home/RemoveItem");
            }

            return Redirect("/Home/Error");

        }

        [HttpGet]
        public IActionResult RemoveItem()
        {
            if (TheList.Count > 0)
            {
                RemoveItemViewModel removeItemViewModel = new RemoveItemViewModel();

                removeItemViewModel.Remlist = Remlist;

                return View(removeItemViewModel);
            }

            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public IActionResult RemoveItem(RemoveItemViewModel removeItemViewModel)

        {
            if (ModelState.IsValid)
            {

                TheList.RemoveAll(x => x.Name == remname & x.Sidelength == removeItemViewModel.NewElement2);

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

                List<Shape> Seglist = new List<Shape>();
                List<Shape> Sqlist = new List<Shape>();
                List<Shape> Cublist = new List<Shape>();

                foreach (Shape item in TheList)
                {
                    if(item.Name == "Segment")
                    {

                        Seglist.Add(item);

                    }

                    if (item.Name == "Square")
                    {

                        Sqlist.Add(item);

                    }

                    if (item.Name == "Cube")
                    {

                        Cublist.Add(item);

                    }


                }

                List<Shape> Seglisto = Seglist.OrderBy(x => x.Sidelength).ToList();
                List<Shape> Sqlisto = Sqlist.OrderBy(x => x.Sidelength).ToList();
                List<Shape> Cublisto = Cublist.OrderBy(x => x.Sidelength).ToList();

                List<Shape> Bridgelist = new List<Shape>();

                foreach (Shape item in Cublisto)
                {

                    Bridgelist.Add(item);

                }

                foreach (Shape item in Seglisto)
                {

                    Bridgelist.Add(item);

                }

                foreach (Shape item in Sqlisto)
                {

                    Bridgelist.Add(item);

                }

                

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
