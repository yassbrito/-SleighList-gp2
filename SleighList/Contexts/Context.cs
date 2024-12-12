using SleighList.Models;
using Microsoft.EntityFrameworkCore;

namespace SleighList.Contexts 
{
    public class Context : DbContext{
        
        public Context(){
        }

        public Context(DbContextOptions<Context> options) : base(options){
        }

         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){

             if(!optionsBuilder.IsConfigured){
                    
                optionsBuilder.UseSqlServer("Data Source=NOTE40-S28\\SQLEXPRESS; Initial Catalog = SleighList_mvc; User Id=sa; Password=123;  TrustServerCertificate = true");

                }

         }


     public DbSet <Usuario> Usuario {get; set;}

     public DbSet <Produto> Produto {get; set;}






    }
}