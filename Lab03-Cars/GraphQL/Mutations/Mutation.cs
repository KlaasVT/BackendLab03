namespace Lab03_Cars.GraphQL;
public class Mutation
{
    public async Task<AddBrandPayload> AddBrand([Service] ICarService carService, AddBrandInput input)
    {
        var newBrand = new Brand()
        {
            Name = input.name,
            Country = input.country
        };
        var created =  await carService.AddBrand(newBrand);
        return new AddBrandPayload(created);
    }
}

public record AddBrandInput(string name,string country);
public record AddBrandPayload(Brand Brand);
