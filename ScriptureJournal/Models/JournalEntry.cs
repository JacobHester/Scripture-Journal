using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScriptureJournal.Models
{
    public class JournalEntry
    {
        public int ID { get; set; }

        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set; }
        public string Cannon { get; set; }
        public string Book { get; set; }
        public string Chapter { get; set; }
        public string Verse { get; set; }
        public string Entry { get; set; }


    }
}
