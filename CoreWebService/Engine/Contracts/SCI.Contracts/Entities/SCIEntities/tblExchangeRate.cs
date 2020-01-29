using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engine.Contracts.Entities
{
    public class tblExchangeRate
    {
        [Key]
        [Column("ID")]
        public int? ID { get; set; }

        [Column("Source")]
        public string Source { get; set; }

        [Column("Target")]
        public string Target { get; set; }

        [Column("Value")]
        public string Value { get; set; }

    }
}