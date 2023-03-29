using TourOfHeroes.Data;
using TourOfHeroes.Interfaces;
using TourOfHeroes.Services;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json.Serialization;

namespace TourOfHeroes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSqlite<DbHeroContext>("Data Source=Heroes.db");

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IHero, HeroService>();
            builder.Services.AddScoped<ISkills, SkillsService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(opcoes => opcoes.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.CreateDbIfNotExists();

            app.MapControllers();

            app.Run();
        }
    }
}