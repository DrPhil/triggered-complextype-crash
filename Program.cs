using TriggeredComplexType;

await using var context = new MyContext();

var myEntity = new MyEntity
{
    ComplexType = new MyComplexType
    {
        Name = "John",
        Age = 30
    }
};

Console.WriteLine("Adding to context");
context.Add(myEntity);
await context.SaveChangesAsync();


Console.WriteLine("Updating context");
myEntity.ComplexType.Name = "Jane";
await context.SaveChangesAsync();
