using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransactMe.Data;
using TransactMe.Models;
using TransactMe.Models.ViewModels;
using TransactMe.Services;

namespace TransactMe.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transaction.OrderByDescending(x => x.TimeStamp).ToListAsync());
        }


        // GET: Transactions/Search
        public IActionResult Search()
        {
            var searchVewModel = new SearchViewModel();
            return View(searchVewModel);
        }

        // GET: Transactions/SearchResults
        public IActionResult SearchResults(SearchViewModel searchViewModel)
        {
            if (searchViewModel is null) return NotFound();
            var clientSsn = searchViewModel.ClientSsn;

            return View(_context.Transaction.Where(x => x.ClientSsn == clientSsn));
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var transaction = await _context.Transaction
                .SingleOrDefaultAsync(m => m.TransactionId == id);
            if (transaction == null)
                return NotFound();

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            var transactionVewModel = new TransactionViewModel();
            return View(transactionVewModel);
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind(
                "ClientFirstName,ClientLastName,ClientSsn,TransactionType,CurrencyName,Amount")]
            TransactionViewModel transactionViewModel)
        {
            if (!ModelState.IsValid) return View(transactionViewModel);

            var transaction = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                TransactionType = transactionViewModel.TransactionType,
                ClientFirstName = transactionViewModel.ClientFirstName,
                ClientLastName = transactionViewModel.ClientLastName,
                ClientSsn = transactionViewModel.ClientSsn,
                CurrencyName = transactionViewModel.CurrencyName,
                Amount = transactionViewModel.Amount
            };
            var officialRate = new CurrenciesAPIService().GetOfficialRate(transaction.CurrencyName);
            transaction.Rate =
                transaction.TransactionType == "Purchase"
                    ? officialRate
                    : 1.01 * officialRate;
            transaction.Total = transaction.Amount * transaction.Rate;
            transaction.TimeStamp = DateTime.Now;

            _context.Add(transaction);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var transaction = await _context.Transaction.SingleOrDefaultAsync(m => m.TransactionId == id);
            if (transaction == null)
                return NotFound();

            var transactionViewModel = new TransactionViewModel
            {
                TransactionType = transaction.TransactionType,
                ClientFirstName = transaction.ClientFirstName,
                ClientLastName = transaction.ClientLastName,
                ClientSsn = transaction.ClientSsn,
                CurrencyName = transaction.CurrencyName,
                Amount = transaction.Amount
            };
            return View(transactionViewModel);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind(
                "ClientFirstName,ClientLastName,ClientSsn,TransactionType,CurrencyName,Amount")]
            TransactionViewModel transactionViewModel)
        {
            var transaction = await _context.Transaction.FirstOrDefaultAsync(x => x.TransactionId == id);
            if (transaction == null)
                return NotFound();

            if (!ModelState.IsValid) return View(transactionViewModel);

            transaction.TransactionType = transactionViewModel.TransactionType;
            transaction.ClientFirstName = transactionViewModel.ClientFirstName;
            transaction.ClientLastName = transactionViewModel.ClientLastName;
            transaction.ClientSsn = transactionViewModel.ClientSsn;
            transaction.CurrencyName = transactionViewModel.CurrencyName;
            transaction.Amount = transactionViewModel.Amount;
            var officialRate = new CurrenciesAPIService().GetOfficialRate(transaction.CurrencyName);
            transaction.Rate =
                transaction.TransactionType == "Purchase"
                    ? officialRate
                    : 1.01 * officialRate;
            transaction.Total = transaction.Amount * transaction.Rate;
            try
            {
                _context.Update(transaction);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(transaction.TransactionId))
                    return NotFound();
                throw;
            }
            return RedirectToAction("Index");
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var transaction = await _context.Transaction
                .SingleOrDefaultAsync(m => m.TransactionId == id);
            if (transaction == null)
                return NotFound();

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var transaction = await _context.Transaction.SingleOrDefaultAsync(m => m.TransactionId == id);
            _context.Transaction.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TransactionExists(Guid id)
        {
            return _context.Transaction.Any(e => e.TransactionId == id);
        }
    }
}