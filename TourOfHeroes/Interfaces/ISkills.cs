using TourOfHeroes.DTOs;
using TourOfHeroes.Models;

namespace TourOfHeroes.Interfaces
{
    public interface ISkills
    {
        Task<List<SkillGetAllDto>> SearchAllSkills();

        Task<SkillModel> Create(SkillModel skill);

        Task<SkillModel> SearchById(int id);

        Task<SkillModel> Update(SkillModel skill, int id);

        Task<bool> Delete(int id);

        Task<SkillModel> GetSkillByName(string name);
    }
}
