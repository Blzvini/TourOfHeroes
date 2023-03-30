using Microsoft.EntityFrameworkCore;
using System;
using TourOfHeroes.Data;
using TourOfHeroes.DTOs;
using TourOfHeroes.Interfaces;
using TourOfHeroes.Models;

namespace TourOfHeroes.Services
{
    public class HeroService : IHero
    {
        private readonly DbHeroContext _dbContext;
        private readonly ISkills _skills;

        public HeroService(DbHeroContext heroContext, ISkills skills)
        {
            _dbContext = heroContext;
            _skills = skills;
        }

        public async Task<HeroModel> SearchById(int id)
        {
            return await _dbContext.Heroes.Include(h => h.Skills).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<HeroGetAllDto>> SearchAllheroes()
        {
            return await _dbContext.Heroes
                .Where(h => h.DeletedAt == null)
                .Select(hero => new HeroGetAllDto
                {
                    Id = hero.Id,
                    Name = hero.Name
                })
                .ToListAsync();
        }

        public async Task<HeroModel> Create(HeroModel hero)
        {

            // Desabilitar o rastreamento de entidades
            _dbContext.Entry(hero).State = EntityState.Detached;

            // Salvar objeto no banco de dados
            await _dbContext.Heroes.AddAsync(hero);
            await _dbContext.SaveChangesAsync();

            return hero;
        }

        public async Task<HeroModel> Update(HeroModel hero, int id)
        {
            HeroModel heroById = await SearchById(id);

            if (heroById == null)
            {
                throw new Exception("This hero was not found");
            }

            heroById.Name = hero.Name;
            heroById.UpdatedAt = hero.UpdatedAt;

            // Remove as habilidades antigas do herói e adiciona as novas
            heroById.Skills.Clear();
            foreach (var skillDto in hero.Skills)
            {
                var skillModel = await _skills.GetSkillByName(skillDto.Name);
                if (skillModel == null)
                {
                    skillModel = new SkillModel
                    {
                        Name = skillDto.Name,
                        Damage = skillDto.Damage,
                        Description = skillDto.Description,
                        Status = "Actived"
                    };
                    await _skills.Create(skillModel);
                }
                heroById.Skills.Add(skillModel);
            }

            _dbContext.Heroes.Update(heroById);
            await _dbContext.SaveChangesAsync();

            return heroById;
        }


        public async Task<bool> Delete(int id)
        {
            HeroModel heroById = await SearchById(id);

            if (heroById == null)
            {
                throw new Exception("This hero does not exist.");
            }

            heroById.DeletedAt = DateTime.Now;

            _dbContext.Heroes.Update(heroById);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
