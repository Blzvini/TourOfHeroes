using TourOfHeroes.Data;

namespace Pokedex.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DbHeroContext context)
        {

            if (context.Heroes.Any())
            {
                return;   // DB has been seeded
            }

            context.Heroes.AddRange();
            context.SaveChanges();
        }
    }
}