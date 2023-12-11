using ExpenseTracker.Core.DTOs.ExpenseCategoryDto;
using ExpenseTracker.Core.DTOs.ExpenseDto;
using ExpenseTracker.Core.IUC;
using ExpenseTracker.WebAPI.Extensions;
using ExpenseTracker.WebAPI.Utils;
using ExpenseTracker.WebAPI.Utils.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.WebAPI.Controllers
{
    [Route("api/expenses")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenses _service;
        private readonly ILogger<IExpenses> _logger;

        public ExpensesController(IExpenses service, ILogger<IExpenses> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetTop()
        {
            try
            {
                var entities = _service.GetTop();
                return ActionResultDRY.Instance.GetAction(result: entities);
            }
            catch (Exception ex)
            {
                return ActionResultDRY.Instance.GetActionException(ex, OperationTypeResponse.get);
            }
        }
        [HttpGet("getTitle")]
        public IActionResult GetTitle()
        {
            var title = _service.title;
            return ActionResultDRY.Instance.GetActionDict(title);
            //return Ok(title);
        }
        [HttpGet("{id}")]
        public IActionResult GetBy(long id)
        {
            var entity = _service.GetBy(id);
            var result = ActionResultDRY.Instance.GetAction(entity, operation_type: OperationTypeResponse.get);
            return result;
        }

        [HttpPost]
        public IActionResult Post(ExpenseRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.GetErrorMessagesDicLst());
                var result = _service.Add(request);
                return ActionResultDRY.Instance.GetAction(result, OperationTypeResponse.add);
                
            }
            catch (Exception ex)
            {
                return ActionResultDRY.Instance.GetActionException(ex, OperationTypeResponse.add);
            }
        }
        // PUT: api/ProductType/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, ExpenseRequestDto request)
        {
            try
            {
                var result = _service.Edit(id, request);
                return ActionResultDRY.Instance.GetAction(result, OperationTypeResponse.edit);
                
            }
            catch (Exception ex)
            {
                return ActionResultDRY.Instance.GetActionException(ex, OperationTypeResponse.edit);
            }

        }
        // DELETE: api/ProductType/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _service.Delete(id);
                return ActionResultDRY.Instance.GetAction(result, OperationTypeResponse.delete);
                
            }
            catch (Exception ex)
            {
                return ActionResultDRY.Instance.GetActionException(ex, OperationTypeResponse.delete);
            }
        }
    }
}
