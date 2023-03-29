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
            return await _dbContext.Heroes.Include(h => h.Skills).FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null);
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
            foreach (var oldSkill in heroById.Skills.ToList())
            {
                if (!hero.Skills.Any(s => s.Id == oldSkill.Id))
                {
                    heroById.Skills.Remove(oldSkill);
                }
            }
            foreach (var newSkill in hero.Skills)
            {
                if (!heroById.Skills.Any(s => s.Id == newSkill.Id))
                {
                    heroById.Skills.Add(newSkill);
                }
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
