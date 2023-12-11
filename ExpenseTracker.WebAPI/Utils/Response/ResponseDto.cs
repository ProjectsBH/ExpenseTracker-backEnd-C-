namespace ExpenseTracker.WebAPI.Utils.Response
{
    public class ResponseDto //<T> where T : class, new(
    {
        public bool is_success { get; set; }
        public string message { get; set; }
        public string code { get; set; } // response_code
        public string operation_type { get; set; }
        //public OperationTypeResponse operation_type { get; set; }
        public dynamic data { get; set; }
        public Error error { get; set; }
    }
}
