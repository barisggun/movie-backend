using MovieApp.EntityLayer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.EntityLayer.Entities
{
    public class AppSetting:BaseEntity
    {
        public string GiphyApiKey { get; set; }
    }
}
