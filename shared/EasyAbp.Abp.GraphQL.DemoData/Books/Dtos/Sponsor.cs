namespace EasyAbp.Abp.GraphQL.Books.Dtos;

public class Sponsor
{
    public string Name { get; set; }

    public Sponsor()
    {
        
    }

    public Sponsor(string name)
    {
        Name = name;
    }
}