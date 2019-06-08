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

        // Searching Variables
        public IList<JournalEntry> JournalEntry { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchBook { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchEntry { get; set; }

        // Sorting Variables
        public string BookSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {

            // Sorting Section
            CurrentSort = sortOrder;
            BookSort = string.IsNullOrEmpty(sortOrder) ? "book_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            IQueryable<JournalEntry> journalIQ = from s in _context.JournalEntry
                                                 select s;

            switch (sortOrder)
            {
                case "book_desc":
                    journalIQ = journalIQ.OrderByDescending(s => s.Book);
                    break;
                case "Date":
                    journalIQ = journalIQ.OrderBy(s => s.EntryDate);
                    break;
                case "date_desc":
                    journalIQ = journalIQ.OrderByDescending(s => s.EntryDate);
                    break;
                default:
                    journalIQ = journalIQ.OrderBy(s => s.Cannon);
                    break;
            }



            // Searching Section
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

            JournalEntry = await journalIQ.AsNoTracking().ToListAsync();
            JournalEntry = await journalEntries.ToListAsync();
        }
    }
}
