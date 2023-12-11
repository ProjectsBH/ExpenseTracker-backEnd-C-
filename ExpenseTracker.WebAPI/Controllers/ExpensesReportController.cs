using ExpenseTracker.Core.DTOs.ExpenseCategoryDto;
using ExpenseTracker.Core.DTOs.ExpenseDto;
using ExpenseTracker.Core.IUC;
using ExpenseTracker.WebAPI.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using System.Globalization;
using System;
using ExpenseTracker.WebAPI.Utils.Response;
using ExpenseTracker.WebAPI.Utils;

namespace ExpenseTracker.WebAPI.Controllers
{
    [Route("api/expensesReport")]
    [ApiController]
    public class ExpensesReportController : ControllerBase
    {
        private readonly IExpensesReport _service;
        private readonly ILogger<IExpensesReport> _logger;

        public ExpensesReportController(IExpensesReport service, ILogger<IExpensesReport> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet("getTitle")]
        public IActionResult GetTitle()
        {
            var title = _service.title;
            return ActionResultDRY.Instance.GetActionDict(title);
        }

        [HttpGet("getByCategoryId/{id}")]
        //http://localhost:5297/api/expensesReport/getByCategoryId/12
        public IActionResult GetBy(int id)
        {
            return _GetBy(id,null,null);
        }
        [HttpGet("getByDates")]
        public IActionResult GetBy(DateTime fromDate, DateTime toDate)
        {
            return _GetBy(null, fromDate, toDate);
        }
        [HttpGet("getByCategoryIdDates")]
        // http://localhost:5297/api/expensesReport/getByCategoryIdDates?categoryId=1&formDate=2023-11-09&toDate=2023-11-09
        public IActionResult GetBy(int categoryId, DateTime fromDate, DateTime toDate)
        {
            // DateTime _fromDate = DateTime.ParseExact(fromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            // DateTime _toDate = DateTime.ParseExact(toDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var result = _GetBy(categoryId, fromDate, toDate);
            return result;
        }
        private IActionResult _GetBy(int? categoryId, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                var entities = _service.GetBy(categoryId, fromDate, toDate);
                return ActionResultDRY.Instance.GetAction(entities, operation_type: OperationTypeResponse.get);
            }
            catch (Exception ex)
            {
                return ActionResultDRY.Instance.GetActionException(ex, OperationTypeResponse.get);
                //return BadRequest($"Exception : {ex.Message}");
            }
        }



    }
}
