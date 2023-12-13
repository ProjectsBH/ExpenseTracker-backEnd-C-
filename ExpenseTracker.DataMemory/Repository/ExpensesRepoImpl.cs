
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Filter;
using ExpenseTracker.Domain.InterfaceUnitOfWork;
using ExpenseTracker.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.DataMemory.Repository
{
    public class ExpensesRepoImpl : IExpensesRepo
    {
        List<ExpensesMdl> dataLst = new List<ExpensesMdl>();
        private Type type = typeof(ExpensesRepoImpl);
        public ExpensesRepoImpl()
        {
        }
        

        public IEnumerable<ExpensesMdl> GetTop(int topNo = 50)
        {
            try
            {
                return dataLst;
            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} GetTop method error :" + type, ex);
            }
        }

        public IEnumerable<ExpensesMdl> CheckCategoryIdHasExpenses(int categoryId)
        {
            try 
            {
                var items = dataLst.Where(a => a.categoryId == categoryId);
                //if (items != null && items.Any())
                 return items;
                //return null;

            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} GetTop method error :" + type, ex);
            }
        }
        private ExpensesMdl getById(long id)
        {
            int index = dataLst.FindIndex(x => x.id == id); 

            if (index != -1)
            {
                ExpensesMdl item = dataLst[index];
                return item;
            }
            return null;
        }
        public ExpensesMdl GetById(object id)
        {
            try
            {
                long _id = Convert.ToInt64(id);
                var item = getById(_id);
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} GetById method error :" + type, ex);
            }
        }

        public IEnumerable<ExpensesMdl> GetFilter(IFilter filter)
        {
            try
            {

                //theDate Between @formDateParam and  @toDateParam
                //{ categoryIdParam = 0, formDateParam = 12/12/2023 12:00:00 AM, toDateParam = 12/12/2023 11:59:59 PM }

                //categoryId = @categoryIdParam and theDate Between @formDateParam and @toDateParam
                //{ categoryIdParam = 1, formDateParam = 12/12/2023 12:00:00 AM, toDateParam = 12/12/2023 11:59:59 PM }

                var getWhere = filter.GetWhere();
                getWhere = getWhere.Replace(" and ", " && ").Replace(" or ", " || ");

              
                var getFilter = filter.GetFilter().ToString();
                getFilter = getFilter.Trim('{', '}').Trim();

                // تقسيم السلسلة إلى أجزاء مستندة على الفاصلة
                string[] parts = getFilter.Split(',');

                // إنشاء قاموس لتخزين القيم والمفاتيح
                Dictionary<string, string> dictionary = new Dictionary<string, string>();

                // فصل كل قسم وتخزينه في القاموس
                foreach (string part in parts)
                {
                    // تقسيم القسم إلى المفتاح والقيمة
                    string[] keyValue = part.Trim().Split('=');

                    // استخراج المفتاح والقيمة
                    string key = keyValue[0].Trim();
                    string value = keyValue[1].Trim();

                    // إضافة المفتاح والقيمة إلى القاموس
                    if((value=="0" || value == "" || value == null)==false)
                    dictionary.Add(key, value);
                }

                Func<ExpensesMdl, bool> predicate = null;
                DateTime fromDate = DateTime.Now;
                DateTime toDate = DateTime.Now; // DateTime.Parse("2023-12-31");
                foreach (KeyValuePair<string, string> entry in dictionary)
                {
                    if (getWhere.Contains("categoryId") && entry.Key.Contains("categoryId"))
                    {
                        predicate = expense => expense.categoryId == Convert.ToInt32(entry.Value);
                    }
                    if (getWhere.Contains("theDate") && getWhere.Contains("Between"))
                    {
                        if (getWhere.Contains("formDate") && entry.Key.Contains("formDate"))
                            fromDate = Convert.ToDateTime(entry.Value);
                        if (getWhere.Contains("toDate") && entry.Key.Contains("toDate"))
                            toDate = Convert.ToDateTime(entry.Value);
                    }
                    //string dd = "Key: " + entry.Key + ", Value: " + entry.Value;
                }
                predicate = CombinePredicates(predicate, expense => expense.theDate >= fromDate && expense.theDate <= toDate);

                return dataLst.Where(predicate);

            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} GetByFilter method error :" + type, ex);
            }
        }
        // دالة لجمع الشروط الأصلية والشروط الإضافية
        public static Func<T, bool> CombinePredicates<T>(Func<T, bool> predicate1, Func<T, bool> predicate2)
        {
            if(predicate1 == null)
                return item => predicate2(item);
            return item => predicate1(item) && predicate2(item);
        }
        public Tuple<bool, object> Add(ExpensesMdl entity)
        {
            try
            {
                long id = dataLst.Any() ? dataLst.Max(a => a.id):0;
                id += 1;
                entity.id = id;
                dataLst.Add(entity);
                return Tuple.Create(true, (object)entity.id);
            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} Add method error :" + type, ex);
            }
        }

        public bool Delete(long entity_id)
        {
            try
            {
                int index = dataLst.FindIndex(x => x.id == entity_id); // الحصول على مؤشر العنصر المراد حذفه بناءً على بعض الشرط

                if (index != -1)
                {
                    dataLst.RemoveAt(index); // حذف العنصر من القائمة
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} Delete method error :" + type, ex);
            }
        }
        public bool Update(ExpensesMdl entity)
        {
            try
            {
                int index = dataLst.FindIndex(x => x.id == entity.id); // الحصول على مؤشر العنصر المراد تعديله بناءً على بعض الشرط

                if (index != -1)
                {
                    var item = getById(entity.id);
                    ExpensesMdl updatedItem = new ExpensesMdl()
                    {
                        id = entity.id,
                        categoryId = entity.categoryId,
                        theDate = entity.theDate,
                        amount = entity.amount,
                        theStatement=entity.theStatement,
                        created_by = item.created_by,
                        created_in = item.created_in
                    };

                    dataLst[index] = updatedItem; // تحديث الكائن في القائمة
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} Update method error :" + type, ex);
            }
        }


    }
}
