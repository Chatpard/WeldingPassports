using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class UIColor : IDomainModel
    {
        public int ID { get; set; }

        public ExtendableStatus ExtendableStatus { get; set; }

        public bool? HasPassed { get; set; }

        [Column(TypeName = "varchar(64)")]
        public string Color { get; set; }
    }
}
