namespace StoreDomain.Entities;

public class Costumer(string name, string email) : Entity
{
    public string Name { get; private set; } = name;
    public string Email { get; private set; } = email;
}
