using System.ComponentModel.DataAnnotations;


namespace Domain
{
    public class Login : BaseEntity
    {
       
            [Required, EmailAddress]
            public string Email { get; set; } = "";

            [Required]
            public string Password { get; set; } = "";

            
        
    }
}
