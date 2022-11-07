using System.ComponentModel.DataAnnotations;

namespace Org.Ktu.T120B178.Backend.Api.Models;

public class RefreshAuthenticatedUserBindingModel
{
    [Required(ErrorMessage = "Access token is required.")]
    public string AccessToken { get; set; }

    [Required(ErrorMessage = "Refresh token is required.")]
    public string RefreshToken { get; set; }
}