using System;
using System.Collections.Generic;
using System.Text;

namespace MVCDemo.DataAccess
{
    // implementing many-to-many relationship
    public class MovieCastMemberJunction
    {
        // primary key of the junction table
        // (by convention, even without [Key])
        public int Id { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual CastMember CastMember { get; set; }
        // navigation properties imply foreign keys
        // when we do migrations / ensurecreated.
    }
}
