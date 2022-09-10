namespace ISPH.Domain.Configuration;

public record EmailSettings
{
    public string Address { get; set; }
    public string Password { get; set; }
    public string ConfirmationLink { get; set; }
    public string EmailUpdateLink { get; set; }
}