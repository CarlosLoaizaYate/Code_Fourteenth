public class Person
{
    public int? Id { get; set; }
    public string? IdentityNumber { get; set; }
    public string? Names { get; set; }
    public string? Surnames { get; set; }
    public DateTime DateBirth { get; set; }
    public List<ContactInformation>? Contact_Information { get; set; }
}
