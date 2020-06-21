using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace DbManager.Models
{
    [Table("Product")]
    public class ShopItem
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Column("Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Column("Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Column("Price")]
        public double Price { get; set; }
        [Column("Image")]
        public string Image { get; set; }
        [Column("SupplierId")]
        public string SupplierId { get; set; }
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        [NotMapped]
        public Category Category { get; set; }

        public ShopItem(string name, string description, double price, string image, Guid categoryId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            Image = image;
            CategoryId = categoryId;
        }

        public ShopItem(string name, string description, double price, string image, Guid categoryId, string supplierId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            Image = image;
            CategoryId = categoryId;
            SupplierId = supplierId;
        }

        public ShopItem() { }
    }
}
