using ExpenseTracker.Core.DTOs.ExpenseCategoryDto;
using ExpenseTracker.Core.IUC;
using ExpenseTracker.WebAPI.Extensions;
using ExpenseTracker.WebAPI.Utils;
using ExpenseTracker.WebAPI.Utils.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace ExpenseTracker.WebAPI.Controllers
{
    [Route("api/expenseCategory")]
    [ApiController]
    public class ExpenseCategoryController : ControllerBase
    {
        private readonly IExpenseCategory _service;
        private readonly ILogger<IExpenseCategory> _logger;

        public ExpenseCategoryController(IExpenseCategory service, ILogger<IExpenseCategory> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var entities = _service.GetAll();
                var result = ResponseResult.Instance.getResponseSuccess(data: entities, operation_type: OperationTypeResponse.get);
                return Ok(result);
            }
            catch (Exception ex)
            {
                //return BadRequest($"Exception : {ex.Message}");
                return ActionResultDRY.Instance.GetActionException(ex, OperationTypeResponse.get);
            }
        }
        [HttpGet("valueId")]
        public IActionResult GetValueId()
        {
            try
            {
                var entities = ExpenseCategoryValueIdDto.Instance.GetAll();
                var result = ActionResultDRY.Instance.GetAction(entities);
                return result;
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

        [HttpPost]
        public IActionResult Post(ExpenseCategoryRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.GetErrorMessagesDicLst());
                var result = _service.Add(request);
                return ActionResultDRY.Instance.GetAction(result, OperationTypeResponse.add);


                /*
                 if (!ModelState.IsValid)
                    return BadRequest(ModelState.GetErrorMessagesDicLst());
                var operation_result = _service.Add(request);
                if (operation_result.Success)
                    return Ok(operation_result);
                else
                    return Conflict(operation_result);
                ///////
                ///if (result.Success)
                    return Ok(ResponseResult.Instance.getResponseSuccess(message: result.Message, operation_type: OperationTypeResponse.add,data: result));
                else
                    return Conflict(ResponseResult.Instance.getResponseError(error_code: result.ResponseCode,error_message: result.Message, operation_type: OperationTypeResponse.add));

                 */
            }
            catch (Exception ex)
            {
                return ActionResultDRY.Instance.GetActionException(ex, OperationTypeResponse.add);
            }
        }
        // PUT: api/expenseCategory/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, ExpenseCategoryRequestDto request)
        {
            try
            {
                var result = _service.Edit(id, request);
                return ActionResultDRY.Instance.GetAction(result, OperationTypeResponse.edit);
                /*
                if (result.Success)
                    return Ok(result);
                else
                    return Conflict(result);
                */
            }
            catch (Exception ex)
            {
                return ActionResultDRY.Instance.GetActionException(ex, OperationTypeResponse.edit);
            }

        }
        // DELETE: api/expenseCategory/5
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
