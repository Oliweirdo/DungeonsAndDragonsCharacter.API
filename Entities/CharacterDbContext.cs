using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonsAndDragonsCharacter.API.Entities
{
    public class CharacterDbContext : DbContext
    {
        private string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=D&DCharacterDATABASE;Trusted_Connection=true";

        public DbSet<Character> Characters { get; set; }
        public DbSet<CharacterBackground> CharacterBackgrounds { get; set; }
        public DbSet<CharacterEquipment> CharacterEquipments { get; set; }
        public DbSet<CharacterSkills> CharacterSkillsSet { get; set; }
        public DbSet<CharacterProperty> CharacterProperties { get; set; }

        public DbSet<Gamer> Gamers { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder
                    .Entity<Character>()
                    .Property(r=>r.Name)
                    .IsRequired();

                modelBuilder
                    .Entity<Gamer>()
                    .Property(r => r.Email)
                    .IsRequired();

                modelBuilder
                    .Entity<Role>()
                    .Property(u => u.Name)
                    .IsRequired();


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    
    }
}
