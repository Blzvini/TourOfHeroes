﻿namespace TourOfHeroes.DTOs
{
    public class UpdateHeroInputDto
    {
        public string UpdateBy { get; set; }
        public string Name { get; set; }
        public List<SkillInputDto> Skills { get; set; }
    }
}
