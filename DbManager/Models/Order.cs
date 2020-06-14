using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DbManager.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Column("Item")]
        public string ItemName { get; set; }
        [ForeignKey("AspNetUsers")]
        public string UserId { get; set; }
        [Column("SupplierId")]
        public string SupplierId { get; set; }

        public Order() { }

        public Order(string itemName, string userId, string supplierId)
        {
            ItemName = itemName;
            UserId = userId;
            SupplierId = supplierId;
        }
    }
}
