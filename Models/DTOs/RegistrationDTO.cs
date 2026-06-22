namespace ShepherdsPie.Models.DTOs;

public class RegistrationDTO
{
    public string UserName { get; set; }
    public string Email { get; set; }

    // Base64-encoded by the client; decoded in AuthController.Register.
    public string Password { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
}
