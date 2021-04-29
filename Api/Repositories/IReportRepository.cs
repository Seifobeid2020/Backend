using Api.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface IReportRepository
    {
        Task<List<ReportViewModel>> GetAllReports();
        Task<List<AdvanceReportViewModel>> GetAllAdvanceReports();
        
    }
}
