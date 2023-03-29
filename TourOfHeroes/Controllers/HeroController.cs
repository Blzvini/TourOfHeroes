using Microsoft.AspNetCore.Mvc;
using TourOfHeroes.Interfaces;
using TourOfHeroes.Models;
using TourOfHeroes.DTOs;


namespace TourOfHeroes.Controllers
{
    [ApiController]
    [Route("api/heroes")]
    public class HeroController : ControllerBase
    {
        private readonly IHero _service;
        private readonly ISkills _skillsService;

        public HeroController(IHero service, ISkills skills)
        {
            _service= service;
            _skillsService = skills;
        }

        // Get all Heroes
        [HttpGet]
        public async Task<ActionResult<List<HeroGetAllDto>>> SearchAllheroes()
        {
            List<HeroGetAllDto> hero = await _service.SearchAllheroes();
            return Ok(hero);
        }

        // Get hero by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<HeroGetByIdDto>> SearchById(int id)
        {
            try
            {
                HeroModel hero = await _service.SearchById(id);

                if (hero == null)
                {
                    throw new Exception($"Hero for this id: {id} not found.");
                }

                var result = new HeroGetByIdDto();

                result.Id = id;
                result.Name = hero.Name;

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }

        [HttpPost]
        public async Task<ActionResult<HeroModel>> Create([FromBody] CreateHeroDto hero)
        {
            try
            {
                HeroModel newHero = new HeroModel
                {
                    CreatedAt = DateTime.Now,
                    Name = hero.Name,
                    CreatedBy = hero.CreatedBy,
                    Skills = new List<SkillModel>()
                };

                foreach (var skillDto in hero.Skills)
                {
                    var skillModel = await _skillsService.GetSkillByName(skillDto.Name);

                    if (skillModel == null)
                    {
                        skillModel = new SkillModel
                        {
                            Name = skillDto.Name,
                            Damage = skillDto.Damage,
                            Description = skillDto.Description
                        };
                        await _skillsService.Create(skillModel);
                    }

                    newHero.Skills.Add(skillModel);
                }

                HeroModel createdHero = await _service.Create(newHero);
                return Ok(createdHero);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }




        // Update a Hero
        [HttpPut("{id}")]
        public async Task<ActionResult<HeroModel>> Update(UpdateHeroInputDto heroModel, int id)
        {
            HeroModel heroById = await _service.SearchById(id);

            if (heroById == null)
            {
                throw new Exception($"Hero for this id: {id} not found.");
            }

            if (heroById.DeletedAt == null)
            {
                heroById.Name = heroModel.Name;
                heroById.UpdatedBy = heroModel.UpdateBy;
                heroById.UpdatedAt = DateTime.Now;
            }

            await _service.Update(heroById, id);

            return heroById;
        }

        // Rollback of a deleted Hero
        [HttpPatch]
        public async Task<ActionResult> Rollback([FromBody] List<int> ids)
        {
            foreach (int id in ids)
            {
                HeroModel hero = await _service.SearchById(id);

                if (hero == null)
                {
                    throw new Exception("This hero does not exist.");
                }

                hero.DeletedAt = null;
                hero.UpdatedAt = DateTime.Now;
                await _service.Update(hero, id);

            }
            return Ok();
        }


        //Busca o Hero pelo seu Id, caso não encontre, retorna uma mensagem de o herói não existe.
        //Se encontrar o Hero que foi buscado, ele atualiza o campo DeletedAT para a data atual, e faz o update.
        // Delete a Hero (SoftDelete)
        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] List<int> ids)
        {
            foreach (int id in ids)
            {
                HeroModel hero = await _service.SearchById(id);

                if (hero == null)
                {
                    throw new Exception("This hero does not exist.");
                }

                hero.DeletedAt = DateTime.Now;
                await _service.Update(hero, id);

            }
            return Ok();
        }

    }
}