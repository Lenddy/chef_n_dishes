#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
//your namespace
namespace chef_n_dishes.Models;    //must be the same that is on you program file 
//classname
public class Chef
{
//* you need to use
//dotnet ef migrations add FirstMigration
//dotnet ef database update
//* only doit after creating you routes with all the info that you need
//this is the primary key
    [Key]
    public int chefId { get; set; }
//change the field as needed

    [Required]
    [MinLength(2)]
    [MaxLength(45)]
    [Display(Name = "First Name")]
    public string f_name { get; set; } 

    [Required]
    [MinLength(2)]
    [MaxLength(45)]
    [Display(Name = "Last Name")]
    public string l_name { get; set; }

    //! this need a custom validation SS
    [Required]
    public DateTime dob { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;


    //  here we are adding a list of dishes that chef made /we made the chef nullable be caus this list can be == 0 and if that happens we are going to create a new on

    //!this dos not need a validation
    public List<Dish> dishesList {get;set;} = new List<Dish>(); 


// this is going to give e the age of the chef


public static int CalculateAge(DateTime dob)
        {
            int age = 0;
            age = DateTime.Now.Year - dob.Year;
            if (DateTime.Now.DayOfYear < dob.DayOfYear)
            {
                age--;
            }
            return age;
            }

}