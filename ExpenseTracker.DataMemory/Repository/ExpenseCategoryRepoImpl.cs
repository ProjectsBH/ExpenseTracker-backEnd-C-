
using ExpenseTracker.Domain.DefaultValues;
using ExpenseTracker.Domain.Entities;
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
    public class ExpenseCategoryRepoImpl : IExpenseCategoryRepo
    {
        //internal const string tableName = SchemaDB.GetSchemaName + "expenseCategoryTb";
        //private readonly IDbStrategyUOW _dbStrategyUOW;
        //private readonly IDbConnection _connection;
        List<ExpenseCategoryMdl> dataLst = new List<ExpenseCategoryMdl>();
        private Type type = typeof(ExpenseCategoryRepoImpl);
        public ExpenseCategoryRepoImpl()
        {
        }

        public IEnumerable<ExpenseCategoryMdl> GetAll()
        {
            try
            {
                var items = dataLst;
                if (items.Any() == false)
                {
                    DefaultValuesAdd();
                    items = new ExpenseCategoryValues().SetValues();
                }
                return dataLst;
            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} All method error :" + type, ex);
            }
        }

        private ExpenseCategoryMdl getById(int categoryId)
        {
           // var item = dataLst.Where(a => a.id == id);
           // if (item != null && item.Any())
           //     return item.Single();
           //return null;

            int index = dataLst.FindIndex(x => x.id == categoryId); // الحصول على مؤشر العنصر المطلوب بناءً على بعض الشرط

            if (index != -1)
            {
                ExpenseCategoryMdl category = dataLst[index]; // الحصول على العنصر من القائمة
                return category;
            }
            return null;
        }
        public ExpenseCategoryMdl GetById(object id)
        {
            try
            {
                int _id = Convert.ToInt32(id);
                var item = getById(_id);
                return item;//?? new ExpenseCategoryMdl();
            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} GetById method error :" + type, ex);
            }
        }
        public ExpenseCategoryMdl GetForExists(string name, object? id)
        {
            try
            {
                IEnumerable<ExpenseCategoryMdl> items;
                if(id == null)
                items = dataLst.Where(a => a.name == name);
                else
                    items = dataLst.Where(a => a.name == name && a.id != (int)id);
                if (items != null && items.Any())
                    return items.Single();
                return null;
               
            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} GetForExists method error :" + type, ex);
            }
        }
        public Tuple<bool, object> Add(ExpenseCategoryMdl entity)
        {
            try
            {
                int id = dataLst.Any() ? dataLst.Max(a => a.id) : 0;
                if (id < 1)
                {
                    DefaultValuesAdd();
                    id += 1;
                }
                else
                    id += 1;
                entity.id = id;
                return _Add(entity);

            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} Add method error :" + type, ex);
            }
        }
        private Tuple<bool, object> _Add(ExpenseCategoryMdl entity)
        {
            try
            {
                dataLst.Add(entity);
                return Tuple.Create(true, (object)entity.id);

            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} Add method error :" + type, ex);
            }
        }
        public bool Update(ExpenseCategoryMdl entity)
        {
            try
            {
                //var item = getById(entity.id);
                //if (item == null)
                //    return false;

                int index = dataLst.FindIndex(x => x.id == entity.id); // الحصول على مؤشر العنصر المراد تعديله بناءً على بعض الشرط

                if (index != -1)
                {
                    var item = getById(entity.id);
                    ExpenseCategoryMdl updatedCategory = new ExpenseCategoryMdl()
                    {
                        id= entity.id,
                        name=entity.name,
                        isLimitAmount = entity.isLimitAmount,
                        limitAmount = entity.limitAmount,
                        created_by = item.created_by,
                        created_in = item.created_in
                    };                                                                                    

                    dataLst[index] = updatedCategory; // تحديث الكائن في القائمة
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} Update method error :" + type, ex);
            }
        }
        public bool Delete(int entity_id)
        {
            try
            {
                //var result = dataLst.Where(a=> a.id == entity_id);
                //if(result != null && result.Any())
                //{
                //    dataLst.Remove(result.Single());
                //}
                //return (result.Count() > 0);

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

        private void DefaultValuesAdd()
        {
            var lst = new ExpenseCategoryValues().SetValues();
            foreach (var item in lst)
            {
                var tuple = _Add(item);
            }
        }



    }
}
