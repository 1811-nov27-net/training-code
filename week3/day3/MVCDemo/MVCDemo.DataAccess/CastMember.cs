using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.DataAccess
{
    public class CastMember
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // navigation property
        // creates many-to-one relationship
        public virtual Movie Movie { get; set; }
    }
}
