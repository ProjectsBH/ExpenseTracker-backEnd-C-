using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Utils.FinalResults
{
    public class ResultsCodes
    {

        static Dictionary<ResultsTypes, string> dctnyMessage = new Dictionary<ResultsTypes, string>()
        {
            {ResultsTypes.Incorrect_Input,"البيانات المدخلة غير صحيحة" },
            {ResultsTypes.Duplicate,"البيانات مكررة" },
            {ResultsTypes.Reference_Not_Found,"المرجعية غير موجودة" },
            {ResultsTypes.Record_Not_Found,"السجل المطلوب غير موجود"},
            {ResultsTypes.Done,"تمت العملية بنجاح" },
            {ResultsTypes.Exception,"حدث خطأ يرجى التواصل مع الدعم الفني"},
            {ResultsTypes.System_Policies,"لن يسمح بمخالفة سياسات النظام" },
            //{ResultsTypes.None,"" },
            {ResultsTypes.None,"خطأ غير معروف" },
        };
        static Dictionary<ResultsTypes, string> dctnyCode = new Dictionary<ResultsTypes, string>()
        {
            {ResultsTypes.Incorrect_Input,"400" },
            {ResultsTypes.Duplicate,"401" },
            {ResultsTypes.Reference_Not_Found,"402" },
            {ResultsTypes.Record_Not_Found,"403"},
            {ResultsTypes.Done,"200" },
            {ResultsTypes.Exception,"600"},
            {ResultsTypes.System_Policies,"1000" },
            //{ResultsTypes.None,"" },
            {ResultsTypes.None,"999" },
        };
        public static string GetMessage(ResultsTypes errorType)
        {
            bool tryGetValue = dctnyMessage.TryGetValue(errorType, out string value);
            if (tryGetValue)
                return value;
            else return dctnyMessage[ResultsTypes.None]; //string.Empty;
        }
        public static string GetCode(ResultsTypes errorType)
        {
            bool tryGetValue = dctnyCode.TryGetValue(errorType, out string value);
            if (tryGetValue)
                return value;
            else return dctnyCode[ResultsTypes.None];
        }
        public static Tuple<string, string> GetCodeMessage(ResultsTypes errorType)
        {
            return Tuple.Create(GetCode(errorType), GetMessage(errorType));

        }
    }
}
