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
                result.Status = hero.Status;

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
                    Status = "Actived",
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
                            Description = skillDto.Description,
                            Status = "Actived"
                        };
                        await _skillsService.Create(skillModel);
                    }

                    newHero.Skills.Add(skillModel);
                }

                HeroModel createdHero = await _service.Create(newHero);

                var response = new
                {
                    Hero = createdHero,
                    Skills = newHero.Skills
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Update a Hero
        [HttpPut("{id}")]
        public async Task<ActionResult<HeroModel>> UpdateHero(int id, [FromBody] UpdateHeroInputDto hero)
        {
            try
            {
                // Busca o herói pelo ID
                var existingHero = await _service.SearchById(id);

                // Verifica se o herói foi encontrado
                if (existingHero == null)
                {
                    throw new Exception($"Hero for this id: {id} not found.");
                }

                // Atualiza as propriedades do herói
                existingHero.Name = hero.Name;
                existingHero.UpdatedBy = hero.UpdateBy;
                existingHero.UpdatedAt = DateTime.Now;

                // Cria uma nova lista de habilidades para o herói atualizado
                var updatedSkills = new List<SkillModel>();

                // Itera sobre as habilidades enviadas na solicitação
                foreach (var skillDto in hero.Skills)
                {
                    // Verifica se a habilidade já existe no banco de dados
                    var existingSkill = await _skillsService.GetSkillByName(skillDto.Name);

                    // Se a habilidade não existe, cria uma nova
                    if (existingSkill == null)
                    {
                        var newSkill = new SkillModel
                        {
                            Name = skillDto.Name,
                            Damage = skillDto.Damage,
                            Description = skillDto.Description,
                            Status = "Actived"
                        };

                        existingSkill = await _skillsService.Create(newSkill);
                    }

                    // Adiciona a habilidade à lista de habilidades atualizadas
                    updatedSkills.Add(existingSkill);
                }

                // Atualiza as habilidades do herói com a nova lista de habilidades
                existingHero.Skills = updatedSkills;

                // Salva as alterações no banco de dados
                await _service.Update(existingHero, id);

                // Retorna o herói atualizado
                return Ok(existingHero);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
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
                hero.Status = "Actived";
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
                hero.Status = "Deleted";
                await _service.Update(hero, id);

            }
            return Ok();
        }

    }
}