using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftvWCFService.Entities
{
    public class Menu
    {        
        public int IdModule { get; set; }
        public string Title { get; set; }
        public string Class { get; set; }
        public string Icon { get; set; }
        public bool OptAdd { get; set; }
        public bool OptSelect { get; set; }
        public bool OptUpdate { get; set; }
        public bool OptDelete { get; set; }
        public int ? ParentId { get; set; }
        public int? SortOrder { get; set; }
        public List<Menu> MenuChild { get; set; }
        

    }

    
}
