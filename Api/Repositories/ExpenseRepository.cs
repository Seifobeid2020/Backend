using Api.Data;
using Api.Models;
using Api.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ExpenseDbContext _context;
        public ExpenseRepository(ExpenseDbContext context)
        {
            _context = context;
        }

        public async Task<List<Expense>> GetAll()
        {
            return await _context.Expenses.Include(e => e.ExpenseType).ToListAsync();
        }
        public async Task<Expense> Get(int id)
        {
            return await _context.Expenses
               .Include(e => e.ExpenseType)
               .Where(e => e.ExpenseId == id)
               .FirstOrDefaultAsync();
        }

        public async Task<Expense> Update(int id, Expense expense)
        {
            _context.Entry(expense).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            var result = await _context.Expenses.FindAsync(id);

            return result;
        }
        public async Task<Expense> Add(Expense expense)
        {
            expense.CreatedAt = DateTime.Now;
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            var newExpense = await _context.Expenses
                                            .Where(e => e.ExpenseId == expense.ExpenseId)
                                            .Include(e => e.ExpenseType)
                                            .FirstAsync();
            return newExpense;
        }

        public async Task<Expense> Delete(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return null;
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return expense;
        }
        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.ExpenseId == id);
        }

        public async Task<List<FiveExpenseViewModel>> GetLastFiveExpenses()
        {
            return await(from ex in _context.Expenses
                         join exType in _context.ExpenseTypes on ex.ExpenseTypeId equals exType.ExpenseTypeId
                         orderby ex.CreatedAt descending
                         select new FiveExpenseViewModel()
                         {
                             ExpenseTypeName =exType.ExpenseTypeName,
                             ExpenseValue = ex.ExpenseValue,
                             CreatedAt = ex.CreatedAt,
                           
                         }).Take(5).ToListAsync();
        }

        public async Task<decimal> GetTotalExpenses()
        {
            return await _context.Expenses.SumAsync(e => e.ExpenseValue);
        }
    }
}
