using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using chef_n_dishes.Models;
using Microsoft.EntityFrameworkCore;

namespace chef_n_dishes.Controllers;

public class chefController : Controller
{


    //here we are setting a private instance of our DB class in our dishes_Context.cs file
    private DB _context;//_context can be what ever we want         it works i test it 

    //* here we are injecting  our context service into the constructor
    // we are making a constructor
    public chefController (DB context){
        _context = context;
    }
    [HttpGet("/chef/all")]
    public IActionResult all()
    {
        // here we are creating a List<> of dishes  and assigning it  to _context.dish(variableName in dishesContext.cs).ToList(); 
        List<Chef> allChef = _context.chefs.Include(c => c.dishesList).ToList();
        return View("chef_all",allChef);
    }

    [HttpGet("/chef/new")]
    public IActionResult chef_New(){
        //grabbing each row in the table then creating a list from the chef table
        ViewBag.allChef = _context.chefs.ToList();
        return View("chef_New");
    }

    [HttpPost("/chef/create")]
    // we are creating new dishes instances thats why we are passing  dishes as datatype for our parameter 
    public IActionResult create(Chef newChef){
        if(ModelState.IsValid == false){
            return chef_New();
        }

        // newDish.chefId = (int)chefId
        //* this only runs if our modelState is valid
        //this is saying  go to my DB folder then the variable that is call chefs and add a new chef
        // this adds it to list of post but i does not saves it in the data base yet 
        _context.chefs.Add(newChef);

        // * this saves it in the data base
        _context.SaveChanges();
        return Redirect("/chef/all");

    }
    [HttpGet("/chef/view/{chefId}")]
    // we are passing the id to the url

    public IActionResult view_one(int chefId){
        // here we are searching for the first id that matches  then setting it to be the on tha we put on the url
        Chef? id = _context.chefs.FirstOrDefault(id => id.chefId == chefId);

        // here we are checking if the id is == to null(does not exist) the we will be redirected the index page
        if(id == null){
            return RedirectToAction("/chef/all");
        }
        //!dont call your file view call it viewOne or something else
        // you need to pass in you id 
        return View("one_Chef",id);
    }
    [HttpPost("/dish/delete/{delete}")]
    // this is for deleting 
    public IActionResult delete(int delete){
        // here we are searching for the first id that matches  then setting it to be the on tha we put on the url
        Chef? deleteId = _context.chefs.FirstOrDefault(c => c.chefId == delete);
        // if it finds something the it will delete it 
        if(deleteId != null){
            _context.chefs.Remove(deleteId);
            _context.SaveChanges();
        }
        // Else it will  redirect you to the index page
        return RedirectToAction("all");
        

    }

    [HttpGet("/dish/edit/{edit}")]

    public IActionResult edit(int edit){
         // here we are searching for the first id that matches  then setting it to be the on tha we put on the url
        Chef? editId = _context.chefs.FirstOrDefault(c => c.chefId == edit);
        //if it finds nothing then it will redirect you to the index page 
        if(editId == null){
            return RedirectToAction("all");
        }
        // else it will render the edit page
        return View("edit_chef",editId);
    }

    [HttpPost("/chef/update/{UpdatedChefId}")]
    public IActionResult update( int UpdatedChefId,Chef UpdatedChef){
        if(ModelState.IsValid == false){
            //* you can do all the logic bello in 1 line by calling the get edit function that you made above because its already doing the logic in the edit function
            return edit(UpdatedChefId);
            // * long way to doit
            // dishes? originalDish = _context.dish.FirstOrDefault(dish => dish.DishId == UpdatedDish.DishId);
            // if(originalDish == null){
            //     return RedirectToAction("index");
            // }
            // return View("edit",originalDish);
        }
        //here we are searching for the first id that matches  then setting it to be the on tha we put on the url
        Chef? originalChef = _context.chefs.FirstOrDefault(c => c.chefId == UpdatedChefId);
        //if it finds nothing then it will redirect you to the index page 
        if(originalChef == null){
            return RedirectToAction("all");
        }
        // this are the thing that we are updating
        originalChef.f_name = UpdatedChef.f_name;
        originalChef.l_name = UpdatedChef.l_name;
        originalChef.dob = UpdatedChef.dob;
        originalChef.UpdatedAt = DateTime.Now;
        _context.chefs.Update(originalChef);
        _context.SaveChanges();
        // 
            // return RedirectToAction("Index",originalDish.DishId);
        return view_one(originalChef.chefId);
    }

    

}
