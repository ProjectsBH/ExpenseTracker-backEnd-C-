namespace ExpenseTracker.WebAPI.Utils
{
    public class ExceptionData //: IExceptionLog
    {
        public void Log(object obj)
        {

        }
        //public static RestAPIGenericResponseDTO<R> Get<R>(Exception ex)
        //{
        //    new ExceptionData().Log(ex.Message); //DOTO: using Singleton Design Pattern
        //    return new RestAPIGenericResponseDTO<R>().WithException(ex);
        //}
        //public static object Get(Exception ex)
        //{
        //    new ExceptionData().Log(ex.Message); //DOTO: using Singleton Design Pattern
        //    return new { Success = false, ResponseCode = 500, Message = "invalid Exception" };
        //}

    }
}
