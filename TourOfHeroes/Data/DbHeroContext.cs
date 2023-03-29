using Microsoft.EntityFrameworkCore;
using TourOfHeroes.Models;

namespace TourOfHeroes.Data
{
    public class DbHeroContext : DbContext
    {
        public DbHeroContext(DbContextOptions<DbHeroContext> options)
        : base(options)
        {
        }
        public DbSet<HeroModel> Heroes => Set<HeroModel>();

        public DbSet<SkillModel> Skills => Set<SkillModel>();
        public async Task<SkillModel> SearchByName(string name)
        {
            return await Skills.FirstOrDefaultAsync(s => s.Name == name);
        }
    }
}
