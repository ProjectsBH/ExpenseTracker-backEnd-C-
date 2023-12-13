
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.InterfaceUnitOfWork;
using ExpenseTracker.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExpenseTracker.DataMemory.Repository
{
    public class UserRepoImpl : IUserRepo
    {
        List<UserMdl> dataLst = new List<UserMdl>();
        private Type type = typeof(UserRepoImpl);
        public UserRepoImpl()
        {
            dataLst.Add(new UserMdl() {
            id=1,
                userName= "basheer",
                password= "123456",
                email= "basheer@gmail.com",
                created_in = DateTime.Now
            });
        }

        public IEnumerable<UserMdl> GetDuplicate(string userName, string password)
        {
            try
            {
                IEnumerable<UserMdl> items = dataLst.Where(a => a.userName == userName ||(a.userName == userName && a.password == password));
                //if (items != null && items.Any())
                return items;
               // return null;

            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} GetDuplicate method error :" + type, ex);
            }
        }

        public Tuple<bool, UserMdl?> Login(string userName, string password)
        {
            try
            {
                IEnumerable<UserMdl> items = dataLst.Where(a => a.userName == userName && a.password == password);
                UserMdl item = items.Any() ? items.Single() : new UserMdl();
                return Tuple.Create((item != null && item.id > 0) , item);
            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} GetForExists method error :" + type, ex);
            }
        }

        public Tuple<bool, object> SignUp(UserMdl entity)
        {
            try
            {
                int id = dataLst.Max(a => a.id);
                id += 1;
                entity.id= id;
                dataLst.Add(entity);
                return Tuple.Create(true, (object)entity.id);
                
            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} Add method error :" + type, ex);
            }
        }
    }
}
