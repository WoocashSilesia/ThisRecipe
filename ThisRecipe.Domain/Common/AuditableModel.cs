using System;

namespace ThisRecipe.Domain.Common
{
    public class AuditableModel
    {
        public int CreatedById { get; set; }
        public DateTime DateTime { get; set; }
        public int? ModifiedById { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
