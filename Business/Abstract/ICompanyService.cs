using Core.Utilities.Result;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICompanyService
    {
        Task<IDataResult<List<Company>>> GetAll();
        IDataResult<Company> GetById(int id);
        IResult Add(Company entity);
        IResult Delete(int id);
        IResult Update(Company entity);
    }
}
