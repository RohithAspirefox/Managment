using Management.Common.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Management.Common.Models.Entity
{
    public class Document
    {
        public Guid DocumentId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DocumentType DocumentType { get; set; }

        public Guid ProjectEntityId { get; set; }
        public ProjectEntity ProjectEntity { get; set; }

        
    }
}
