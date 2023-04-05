using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Runtime.Remoting.Contexts;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Support_Janssons_fastigheter.Models
{
    public class MyContext : DbContext
    {
        public MyContext() : base("MySqlConnectionString")
        {
        }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .Property(t => t.TicketId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}