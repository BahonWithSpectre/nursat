using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace nursat.Data
{
    public class Child
    {
        public int Id { get; set; }

        [Display(Name = "Аты-жөні")]
        public string ChildName { get; set; }
        [Display(Name = "ИИН(ЖСН)")]
        public string IIN { get; set; }
        [Display(Name = "Пәндер")]
        public string Pan { get; set; }
        [Display(Name = "Тіркелу уақыты")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Кету уақыты")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Ата-анасы")]
        public string Parent { get; set; }

        [Display(Name = "Телефон номер")]
        public string PhoneNumber { get; set; }

        public bool DeleteStats { get; set; } = false;
    }
}
