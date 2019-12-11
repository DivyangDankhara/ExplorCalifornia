using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExplorCalifornia.Models
{
    public class Post
    {
        [Display(Name = "Post Title")]
        [Required]
        [DataType(DataType.Text)]
        [StringLength(100,MinimumLength = 5,ErrorMessage = "Title must be between 5 to 100 characters long")]
        public string Title { get; set; }
        public string Author { get; set; }
        
        [Required]
        [MinLength(100,ErrorMessage ="Blog post must be least 100 characters long")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        public DateTime Posted { get; set; }
    }
}
