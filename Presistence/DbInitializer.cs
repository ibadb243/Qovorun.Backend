using Microsoft.EntityFrameworkCore;

namespace Presistence;

public class DbInitializer
{
    public static void Initialize(params DbContext[] contexts)
    {
        foreach (var context in contexts)
        {
            context.Database.EnsureCreated();
        }
    }
}