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
        [Column("Quantity")]
        public int Quantity { get; set; }
        [Column("Price")]
        public double Price { get; set; }
        [ForeignKey("AspNetUsers")]
        public string UserId { get; set; }
        [Column("SupplierId")]
        public string SupplierId { get; set; }

        [NotMapped]
        public string UserName { get; set; }
        [NotMapped]
        public string SupplierName { get; set; }

        public Order() { }

        public Order(string itemName, double price, int quantity, string userId, string supplierId)
        {
            ItemName = itemName;
            Price = price;
            Quantity = quantity;
            UserId = userId;
            SupplierId = supplierId;
        }
    }
}
