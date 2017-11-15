namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Language")]
    public partial class Language
    {
        public int LanguageId { get; set; }

        [StringLength(250)]
        public string NameLanguage { get; set; }

        public bool Status { get; set; }
    }
}
