using TourOfHeroes.DTOs;
using TourOfHeroes.Models;

namespace TourOfHeroes.Interfaces
{
    public interface IHero
    {
        Task<List<HeroGetAllDto>> SearchAllheroes();

        Task<HeroModel> SearchById(int id);

        Task<HeroModel> Create(HeroModel hero);

        Task<HeroModel> Update(HeroModel hero, int id);

        Task<bool> Delete(int id);
    }
}
