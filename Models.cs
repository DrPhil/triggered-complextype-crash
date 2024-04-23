using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TriggeredComplexType;

public class MyContext : DbContext
{
    public DbSet<MyEntity> MyEntities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        options.UseSqlite($"Data Source={Path.Join(path, "complextypes.db")}");

        options.UseTriggers(triggerOptions =>
        {
            triggerOptions.AddTrigger(typeof(MyTrigger));
        });
    }
}

public class MyEntity
{
    public int Id { get; set; }
    public MyComplexType ComplexType { get; set; }
}

[ComplexType]
public class MyComplexType
{
    public string? Name { get; set; }
    public int Age { get; set; }
}

public class MyTrigger : IBeforeSaveTrigger<MyEntity>
{
    public void BeforeSave(ITriggerContext<MyEntity> context)
    {
        Console.WriteLine("Here we are in the trigger");
        Console.WriteLine($"The unmodified entity is: {context.UnmodifiedEntity}");
    }
}