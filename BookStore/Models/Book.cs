using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    /// <summary>
    /// Class represents book object
    /// A book has properties such as id,name,price,description
    /// Also book has a writer and category
    /// </summary>
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kitap ismi boş bırakılamaz!")]
        [Display(Name = "Ürün Adı")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Kitap açıklaması boş bırakılamaz!")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Kitap yazarı boş bırakılamaz!")]
        [Display(Name = "Yazar")]
        public Writer  Writer{ get; set; }
        public int WriterId { get; set; }
        public double Rating { get; set; }

        [Required(ErrorMessage = "Fiyat boş bırakılamaz!")]
        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }
        public double Discount { get; set; }
        public string ImageUrl { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public int Page { get; set; }
        [Required]

        public int CategoryId { get; set; }
        [ForeignKey ("CategoryId")]
        public Category Category { get; set; }
        
    }
}
