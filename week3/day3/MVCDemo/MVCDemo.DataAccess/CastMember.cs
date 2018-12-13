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
        public virtual ICollection<MovieCastMemberJunction> MovieJunctions { get; set; }
    }
}
