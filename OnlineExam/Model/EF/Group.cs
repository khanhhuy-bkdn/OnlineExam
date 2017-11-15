namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Group")]
    public partial class Group
    {
        public int GroupId { get; set; }

        [StringLength(250)]
        public string NameGroup { get; set; }

        public int? LessionId { get; set; }

        public bool Status { get; set; }
    }
}
