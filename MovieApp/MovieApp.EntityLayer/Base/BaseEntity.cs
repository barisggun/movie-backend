using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.EntityLayer.Base
{
    public class BaseEntity
    {
        [Key]
        public int ID { get; set; }
    }
}
