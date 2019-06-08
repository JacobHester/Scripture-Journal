using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ScriptureJournal.Models;

namespace ScriptureJournal.Pages.JournalEntries
{
    public class IndexModel : PageModel
    {
        private readonly ScriptureJournal.Models.ScriptureJournalContext _context;

        public IndexModel(ScriptureJournal.Models.ScriptureJournalContext context)
        {
            _context = context;
        }

        public IList<JournalEntry> JournalEntry { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchBook { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchEntry { get; set; }

        public async Task OnGetAsync()
        {
            var journalEntries = from m in _context.JournalEntry
                                 select m;

            if (!string.IsNullOrEmpty(SearchBook))
            {
                journalEntries = journalEntries.Where(s => s.Book.Contains(SearchBook));
            }

            if (!string.IsNullOrEmpty(SearchEntry))
            {
                journalEntries = journalEntries.Where(s => s.Entry.Contains(SearchEntry));
            }

            JournalEntry = await journalEntries.ToListAsync();
        }
    }
}
