using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ExpenseTracker.WebAPI.Extensions
{
    public static class ModelStateExtensions
    {
        public static Dictionary<string, List<string>> GetErrorMessagesDicLst(this ModelStateDictionary dictionary)
        {
            Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
            foreach (var item in dictionary)
            {
                dic.Add(item.Key, item.Value.Errors.Select(m => m.ErrorMessage).ToList());
            }
            return dic;
        }

        public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
        {
            return dictionary.SelectMany(m => m.Value.Errors)
                             .Select(m => m.ErrorMessage)
                             .ToList();
        }

        public static Dictionary<string, string> GetErrorMessagesDic(this ModelStateDictionary dictionary)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var item in dictionary)
            {
                foreach (var item1 in item.Value.Errors)
                {
                    dic.Add(item.Key, item1.ErrorMessage);
                }
            }
            return dic;
        }
        
    }
}
