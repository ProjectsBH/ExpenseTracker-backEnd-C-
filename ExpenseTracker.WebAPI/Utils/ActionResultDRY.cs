using ExpenseTracker.Core.Utils.FinalResults;
using ExpenseTracker.WebAPI.Utils.Response;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.WebAPI.Utils
{
    public class ActionResultDRY : ControllerBase
    {
        #region Singleton Design Pattern
        private ActionResultDRY() { }
        // Lazy is Thread Safe Default
        private static readonly Lazy<ActionResultDRY> instanceLock = new Lazy<ActionResultDRY>(() => new ActionResultDRY());
        public static ActionResultDRY Instance
        {
            get
            {
                return instanceLock.Value;
            }
        }
        #endregion

        public IActionResult GetAction(ServicesResultsDto resultsDto, OperationTypeResponse operation_type)
        {
            if (resultsDto.Success)
                return Ok(ResponseResult.Instance.getResponseSuccess(message: resultsDto.Message, operation_type: operation_type, data: resultsDto));
            else
                return Ok(ResponseResult.Instance.getResponseError(error_code: resultsDto.ResponseCode, error_message: resultsDto.Message, operation_type: operation_type));
            /*
            if (resultsDto.Success)
                return Ok(ResponseResult.Instance.getResponseSuccess(message: resultsDto.Message, operation_type: operation_type, data: resultsDto));
            else
                return Conflict(ResponseResult.Instance.getResponseError(error_code: resultsDto.ResponseCode, error_message: resultsDto.Message, operation_type: operation_type));
            */
        }


        public IActionResult GetAction(dynamic result, OperationTypeResponse operation_type = OperationTypeResponse.get)
        {
            return Ok(ResponseResult.Instance.getResponseSuccess(data: result, operation_type: operation_type));
        }

        public IActionResult GetActionDict(dynamic value, OperationTypeResponse operation_type = OperationTypeResponse.get)
        {
            var values = new Dictionary<string, dynamic>() { { "value", value } };
            return GetAction(result: values, operation_type: operation_type);
        }

        public IActionResult GetActionException(Exception ex, OperationTypeResponse operation_type = OperationTypeResponse.get)
        {
            // return BadRequest($"Exception : {ex.Message}");
            return BadRequest(ResponseResult.Instance.getResponseException(ex, operation_type: operation_type));
        }
    }
}
