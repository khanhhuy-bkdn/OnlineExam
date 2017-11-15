namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Lession")]
    public partial class Lession
    {
        public int LessionId { get; set; }

        [StringLength(250)]
        public string NameLession { get; set; }

        public int? LanguageId { get; set; }

        public bool Status { get; set; }
    }
}
