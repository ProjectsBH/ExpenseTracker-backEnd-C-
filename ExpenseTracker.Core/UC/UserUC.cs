using ExpenseTracker.Core.DTOs.ExpenseDto;
using ExpenseTracker.Core.DTOs.UserDto;
using ExpenseTracker.Core.IUC;
using ExpenseTracker.Core.Utils;
using ExpenseTracker.Core.Utils.FinalResults;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.IRepository;
using ExpenseTracker.Shared.DRY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.UC
{
    public class UserUC : IUser
    {
        private readonly IUserRepo _repo;
        public UserUC(IUserRepo repo)
        {
            _repo = repo;
        }
        
        public ServicesResultsDto Login(string userName, string password)
        {
            try
            {
                var result= _repo.Login(userName, password);
                var item = result.Item2;
                if (result.Item1)
                {
                    SessionData.idUserMy = item!.id;
                    return ServicesResultsDRY.GetSuccess();
                }
                else return ServicesResultsDRY.GetError(ResultsTypes.None);
            }
            catch (Exception)
            {
                ServicesResultsDRY.GetException();
                throw;
            }
        }

        public ServicesResultsDto SignUp(UserRequestDto entity)
        {
            try
            {
                // Valid
                var result = ValidationModels.Validated(entity);
                if (result.Item1 == false)
                    return ServicesResultsDRY.GetIncorrectInput(result.Item2);

                if (entity.password != entity.confirmPassword)
                    return ServicesResultsDRY.GetIncorrectInput("كلمة المرور غير متساوية مع تأكيدها");

                var items = _repo.GetDuplicate(entity.userName, entity.password);
                if (items != null && items.Any())
                    return ServicesResultsDRY.GetIncorrectInput("سياسة النظام لا تسمح بهذه البيانات");

                UserMdl mdl = entity.toModel(entity);

                var tupleAdd = _repo.SignUp(mdl);
                var isDone = tupleAdd.Item1;
                if (isDone)
                    return ServicesResultsDRY.GetSuccessWithId(tupleAdd.Item2);
                else return ServicesResultsDRY.GetError(ResultsTypes.None);
            }
            catch (Exception)
            {
                return ServicesResultsDRY.GetException();
                throw;
            }
        }
    }
}
