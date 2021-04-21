using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MbanqPeopleCRUD.Models
{
    public class Person
    {
        public int ID { get; set; }
        public string TIN { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Place { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}