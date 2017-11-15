namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Question")]
    public partial class Question
    {
        public int QuestionId { get; set; }

        [Column(TypeName = "text")]
        public string Content { get; set; }

        [Column(TypeName = "text")]
        public string AnswerA { get; set; }

        [Column(TypeName = "text")]
        public string AnswerB { get; set; }

        [Column(TypeName = "text")]
        public string AnswerC { get; set; }

        [Column(TypeName = "text")]
        public string AnswerD { get; set; }

        public int? CorrectAnswer { get; set; }

        public int? GroupId { get; set; }

        public bool Status { get; set; }
    }
}
