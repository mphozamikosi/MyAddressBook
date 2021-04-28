using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Contacts
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Column(TypeName ="varchar(100)")]
        public string FirstName { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Surname { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Tel { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Cel { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }
        public string UpdatedDate { get; set; }
    }
}
