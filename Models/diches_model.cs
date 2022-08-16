#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//your namespace
namespace chef_n_dishes.Models;
//classnam
public class Dish
{
//this is the primary key
    [Key]
    public int DishId { get; set; }
//change the field as needed
    [Required]
    [MinLength(2)]
    [MaxLength(45)]
    [Display(Name = "Dish Name")]
    public string Name { get; set; }
    // [Required]
    // [MinLength(2)]
    // [MaxLength(45)] 
    // [Display(Name = "Chef Name")] 
    // public string Chef { get; set; }
// you need to use range if you want a specific number to be added
        [Required]
        [Range(1,5)]
    public int Tastiness { get; set; }

    [Required]
    [Range(1,Int32.MaxValue)]

    public int Calories { get; set; }
    [Required]
    [MinLength(2)]
    public string Description{get;set;}
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    /*****************************************************
    relationship properties below

    foreign key should be == to the chef tables key(id)

    navigation property ( this is how data is related together ) data type is a related model

    Must have(use) .include for the nav prop data to include via a sql join  statement 
    *****************************************************/

    // we need the chefs id(foreign key)
    public int chefId {get; set;} // this foreign key needs to match with the key on the chef model

    // complex object cant be store in the data base
    public Chef? cook  {get; set;} //1 chef related to each post






}