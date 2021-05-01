using Api.Models;
using Api.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
   public interface IExpenseRepository
    {
        Task<List<Expense>> GetAll();
        Task<Expense> Get(int id);
        Task<Expense> Add(Expense expense);
        Task<Expense> Update(int id, Expense expense);
        Task<Expense> Delete(int id);
        Task<decimal> GetTotalExpenses();
        Task<List<FiveExpenseViewModel>> GetLastFiveExpenses();

    }
}
