namespace StoreDomain.Entities;

public class Costumer(string name, string email)
{
    public string Name { get; private set; } = name;
    public string Email { get; private set; } = email;
}
