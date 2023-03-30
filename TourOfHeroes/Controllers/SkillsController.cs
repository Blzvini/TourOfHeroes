using Microsoft.AspNetCore.Mvc;
using TourOfHeroes.DTOs;
using TourOfHeroes.Interfaces;
using TourOfHeroes.Models;

namespace TourOfHeroes.Controllers
{
    [ApiController]
    [Route("api/skills")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkills _service;
        public SkillsController(ISkills service)
        {
            _service= service;
        }
        // Get all skills
        [HttpGet]
        public async Task<ActionResult<List<SkillGetAllDto>>> SearchAllSkills()
        {
            List<SkillGetAllDto> hero = await _service.SearchAllSkills();
            return Ok(hero);
        }

        // Get skill by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<SkillGetByIdDto>> SearchById(int id)
        {
            try
            {
                SkillModel skill = await _service.SearchById(id);

                if (skill == null)
                {
                    throw new Exception($"The skill for this id: {id} not found.");
                }

                var result = new SkillGetByIdDto();

                result.Id = id;
                result.Name = skill.Name;
                result.Damage = skill.Damage;
                result.Description = skill.Description;
                result.Status = skill.Status;

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }

        // Create a new Skill
        [HttpPost]
        public async Task<ActionResult<SkillModel>> Create([FromBody] CreateSkillInputDto skill)
        {
            try
            {

                SkillModel newSkill = new SkillModel();
                newSkill.CreatedAt = DateTime.Now;
                newSkill.CreatedBy = skill.CreatedBy;
                newSkill.Name = skill.Name;
                newSkill.Damage = skill.Damage;
                newSkill.Description = skill.Description;
                newSkill.Status = "Actived";

                SkillModel createdSkill = await _service.Create(newSkill);
                return Ok(createdSkill);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Update a skill
        [HttpPut("{id}")]
        public async Task<ActionResult<SkillModel>> Update(UpdateSkillInputDto skillModel, int id)
        {
            SkillModel skillById = await _service.SearchById(id);

            if (skillById == null)
            {
                throw new Exception($"The skill for this id: {id} not found.");
            }

            if (skillById.DeletedAt == null)
            {
                skillById.Name = skillModel.Name;
                skillById.UpdatedBy = skillModel.UpdateBy;
                skillById.UpdatedAt = DateTime.Now;
            }

            await _service.Update(skillById, id);

            return skillById;
        }

        // Rollback of a deleted Skill
        [HttpPatch]
        public async Task<ActionResult> Rollback([FromBody] List<int> ids)
        {
            foreach (int id in ids)
            {
                SkillModel skill = await _service.SearchById(id);

                if (skill == null)
                {
                    throw new Exception("This skill does not exist.");
                }

                skill.DeletedAt = null;
                skill.Status = "Actived";
                skill.UpdatedAt = DateTime.Now;
                await _service.Update(skill, id);

            }
            return Ok();
        }

        // Delete a skill
        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] List<int> ids)
        {
            foreach (int id in ids)
            {
                SkillModel skill = await _service.SearchById(id);

                if (skill == null)
                {
                    throw new Exception("This skill does not exist.");
                }

                skill.Status = "Deleted";
                skill.DeletedAt = DateTime.Now;
                await _service.Update(skill, id);

            }
            return Ok();
        }

    }
}