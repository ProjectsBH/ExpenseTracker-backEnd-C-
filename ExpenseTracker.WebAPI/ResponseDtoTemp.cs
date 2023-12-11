namespace ExpenseTracker.WebAPI
{
    public class ResponseResultTemp
    {
        /*
         * الدالة الاولى ترجع شيئين هما (Response , http code)
         * الدالة الثانية خاصة بالنجاح وتستدعي الدالة الاولى
         * الدالة الثالثة خاصة بوجود أخطأ مع الرد وتستدعي الدالة الاولى  
         * هذه الدوال تكون في طبقة المستخدم عند برنامج API
         */

        /*
         * // الدوال بلغة البايثون
         @staticmethod
    def getResponse(_status,is_success=False, message="",code="",operation_type="", data="",error=""):
        response={
            "is_success": is_success,
            "message":message,
            "code":code,
            "operation_type":operation_type,
            "data":data,
            "error":error
            }
        return Response(response, status=_status)
    
    @staticmethod
    def getResponseSuccess(data,operation_type,message=""):
        return ResponseDto.getResponse(_status=status.HTTP_200_OK,is_success=True,message=message,data=data,operation_type=operation_type)
    
    @staticmethod
    def getResponseError(error_code,error_message,operation_type):
        error ={"error_code":error_code,
                "error_message":error_message}
        return ResponseDto.getResponse(_status=status.HTTP_400_BAD_REQUEST,error=error,operation_type=operation_type,message=error_message)

         */

    }
    public class ResponseDtoTemp//<T> where T : class, new()
    {
        public bool is_success { get; set; }
        public string message { get; set; }
        public string code { get; set; } // response_code
        public OperationTypeTemp operation_type { get; set; }
        //public T data { get; set; }
        public dynamic data { get; set; }
        public ErrorTemp error { get; set; }
    }

    public class ErrorTemp
    {
        public string error_code { get; set; }
        public string error_message { get; set; }
    }
    public enum OperationTypeTemp
    {
        get,
        add,
        edit,
        delete,
        others
    }
}
