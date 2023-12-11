using ExpenseTracker.Core.DTOs.ExpenseCategoryDto;
using ExpenseTracker.Core.Utils.FinalResults;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.WebAPI.Utils.Response
{
    public class ResponseResult 
    {
        /*
         * الدالة الاولى ترجع شيئين هما (Response , http code)
         * الدالة الثانية خاصة بالنجاح وتستدعي الدالة الاولى
         * الدالة الثالثة خاصة بوجود أخطأ مع الرد وتستدعي الدالة الاولى  
         * هذه الدوال تكون في طبقة المستخدم عند برنامج API
         */

        #region Singleton Design Pattern
        private ResponseResult() { }
        // Lazy is Thread Safe Default
        private static readonly Lazy<ResponseResult> instanceLock = new Lazy<ResponseResult>(() => new ResponseResult());
        public static ResponseResult Instance
        {
            get
            {
                return instanceLock.Value;
            }
        }
        #endregion

        #region
        public ResponseDto getResponse(OperationTypeResponse operation_type, bool is_success= false, string message= "", string code= "",  dynamic? data=null, Error error=null)
        {
            return new ResponseDto()
            {
                is_success = is_success,
                message = message,
                code = code,
                operation_type = operation_type.ToString(),
                data = data,
                error= error
            };

        }

        public ResponseDto getResponseSuccess(dynamic data, OperationTypeResponse operation_type, string message="")
        {
            return getResponse(is_success:true,message:message,data:data,operation_type:operation_type,error:null);            
        }
        public ResponseDto getResponseError(string error_code,string error_message, OperationTypeResponse operation_type)
        {
            var errorObj=new Error()
                {error_code=error_code,error_message=error_message };
            return getResponse(error: errorObj, operation_type: operation_type,message:error_message,data:null);

        }
        public ResponseDto getResponseException(Exception ex, OperationTypeResponse operation_type)
        {
            var message = "an exception occurred";
            var errorObj = new Error()
            { error_code = "492", error_message = message };
            return getResponse(error: errorObj, operation_type: operation_type, message: message, data: null);

        }
        #endregion

    }
}
