using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDetails.Models
{
    public class Candidate
    {
        public Candidate() { 
            Experiances= new List<Experiance>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        [DataType(DataType.PhoneNumber)]
        public string Phone {  get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool Fresher { get; set; }
        [ValidateNever]
        public string PicPath { get; set; }
        [NotMapped]
        public IFormFile Image {  get; set; }
        public List<Experiance>? Experiances { get; set; }

    }
}
