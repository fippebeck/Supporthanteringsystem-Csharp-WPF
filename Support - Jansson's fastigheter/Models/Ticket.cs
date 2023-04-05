using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Support_Janssons_fastigheter.Models
{
    [Table("tickets")]
    public class Ticket
    {
        [Key]
        [Column("ticket_id")]
        public int TicketId { get; set; }

        [ForeignKey("Customer")]
        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Column("category_name")]
        public string CategoryName { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("status")]
        public string Status { get; set; }

        public virtual Customer Customer { get; set; }
    }


    [Table("customers")]
    public class Customer
    {
        [Key]
        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Column("customer_name")]
        public string CustomerName { get; set; }

        [Column("customer_email")]
        public string CustomerEmail { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; } // Navigation property
        // other properties
    }
}

public class TicketViewModel
{
    public int TicketId { get; set; }
    public string CustomerName { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
}