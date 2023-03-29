using Microsoft.EntityFrameworkCore;
using TourOfHeroes.Data;
using TourOfHeroes.DTOs;
using TourOfHeroes.Interfaces;
using TourOfHeroes.Models;

namespace TourOfHeroes.Services
{
    public class SkillsService : ISkills
    {
        private readonly DbHeroContext _dbContext;
        public SkillsService(DbHeroContext heroContext)
        {
            _dbContext = heroContext;
        }
        public async Task<SkillModel> SearchById(int id)
        {
            return await _dbContext.Skills.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<SkillGetAllDto>> SearchAllSkills()
        {
            return await _dbContext.Skills.Where(s => s.DeletedAt == null)
                .Select(skill => new SkillGetAllDto
                {
                    Id = skill.Id,
                    Name = skill.Name,
                    Damage = skill.Damage,
                    Description = skill.Description
                }
                ).ToListAsync();
        }

        public async Task<SkillModel> Create(SkillModel skill)
        {
            if (string.IsNullOrEmpty(skill.Name))
            {
                throw new ArgumentException("Skill name cannot be empty.");
            }

            if (skill.Damage < 0)
            {
                throw new ArgumentException("Skill damage cannot be negative.");
            }

            if (string.IsNullOrEmpty(skill.Description))
            {
                throw new ArgumentException("Skill description cannot be empty.");
            }

            await _dbContext.Skills.AddAsync(skill);
            await _dbContext.SaveChangesAsync();

            return skill;
        }


        public async Task<SkillModel> Update(SkillModel skill, int id)
        {
            SkillModel skillById = await SearchById(id);

            if (SearchById == null)
            {
                throw new Exception("This skill was not found");
            }

            skillById.Name = skill.Name;
            skillById.UpdatedAt = skill.UpdatedAt;

            _dbContext.Skills.Update(skillById);
            await _dbContext.SaveChangesAsync();

            return skillById;
        }

        public async Task<bool> Delete(int id)
        {
            SkillModel skillById = await SearchById(id);

            if (SearchById == null)
            {
                throw new Exception("This skill does not exist.");
            }

            _dbContext.Skills.Remove(skillById);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<SkillModel> GetSkillByName(string name)
        {
            var skill = await _dbContext.SearchByName(name);
            return skill;
        }

    }
}