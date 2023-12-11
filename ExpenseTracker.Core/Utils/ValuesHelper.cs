using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Utils
{
    public static class ValuesHelper
    {
        public static object GetValue<T>(this IList<T> t, Func<T, bool> predicate, object name)
        {
            var item = t.Where(predicate).FirstOrDefault();
            //return item.GetType().GetProperty(name.ToString()).GetValue(item, null);
            return item?.GetType().GetProperty(name.ToString()).GetValue(item, null);
        }
        public static string GetValueStr<T>(this IList<T> t, Func<T, bool> predicate, object name)
        {
            //return t.GetValue(predicate, name).ToString();
            return t?.GetValue(predicate, name)?.ToString();
        }
        public static byte GetValueByte<T>(this IList<T> t, Func<T, bool> predicate, object name)
        {
            return Convert.ToByte(t.GetValueStr(predicate, name));
        }
        public static bool GetValueBool<T>(this IList<T> t, Func<T, bool> predicate, object name)
        {
            return Convert.ToBoolean(t.GetValueStr(predicate, name));
        }
        public static T GetName<T>(this IEnumerable<T> t, bool where, string name)
        {
            return t.Where(a => where).FirstOrDefault();
        }
        public static IEnumerable<TSource> WhereBH<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.Where(predicate).ToList();
        }
        public static IList<TSource> WhereBH<TSource>(this IList<TSource> source, Func<TSource, bool> predicate)
        {
            return source.Where(predicate).ToList();
        }

        // public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    }
}
