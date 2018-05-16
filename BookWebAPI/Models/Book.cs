//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            this.Comments = new HashSet<Comment>();
        }
    
        public int BookID { get; set; }
        public string Title { get; set; }
        public int CateID { get; set; }
        public int AuthorID { get; set; }
        public int PubID { get; set; }
        public string Summary { get; set; }
        public string ImgUrl { get; set; }
        public double Price { get; set; }
        public string Quantity { get; set; }
        public System.DateTime CreateDay { get; set; }
        public System.DateTime ModifiedDay { get; set; }
        public bool IsActive { get; set; }
        public int BookStatusID { get; set; }
    
        public virtual Author Author { get; set; }
        public virtual Category Category { get; set; }
        public virtual Publisher Publisher { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual StatusBook StatusBook { get; set; }
        public virtual StatusBook StatusBook1 { get; set; }
    }
}
