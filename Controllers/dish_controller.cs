using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using chef_n_dishes.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudDelicious.Controllers;

public class dishesController : Controller
{
    //here we are setting a private instance of our DB class in our dishes_Context.cs file
    private DB _context;//_context can be what ever we want         it works i test it 

    //* here we are injecting  our context service into the constructor
    // we are making a constructor
    public dishesController (DB context){
        _context = context;
    }
    [HttpGet("")]
    public IActionResult Index()
    {
        // here we are creating a List<> of dishes  and assigning it  to _context.dish(variableName in dishesContext.cs).ToList();        

        // ! you can think of include as a join 
        List<Dish> allDishes = _context.dishes.Include(c => c.cook).ToList();
        ViewBag.allDishes = allDishes;
        return View("Index",allDishes);
    }

    [HttpGet("/dish/new")]
    public IActionResult New(){
        // here we are grabbing all the chef that are in the ViewBag  then sending it to the front end
        ViewBag.allChef = _context.chefs.ToList();
        return View("New");
    }
    [HttpPost("/dish/create")]
    // we are creating new dishes instances thats why we are passing  dishes as datatype for our parameter 
    public IActionResult create(Dish newDish){
        if(ModelState.IsValid == false){
            return New();
        }

        // newDish.chefId = (int)chefId

        //* this only runs if our modelState is valid
        //this is saying  go to my DB folder then the variable that is call dish and add a new dish
        // this adds it to list of post but i does not saves it in the data base yet 
        _context.dishes.Add(newDish);
        // * this saves it in the data base
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

}









