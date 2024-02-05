using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Common.Models.Entity
{
    public class UserProject
    {
        public Guid UserProjectId { get; set; }
        public string UserId { get; set; }
        public Guid ProjectEntityId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("ProjectEntityId")]
        public virtual ProjectEntity ProjectEntity { get; set; }  
    }
}
