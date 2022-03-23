using Business.Abstract;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CompanyManager : ICompanyService
    {
        ICompanyDal _companyDal;

        public CompanyManager(ICompanyDal companyDal)
        {
            _companyDal = companyDal;
        }

        public async Task<IResult> AddAsync(Company entity)
        {
            await _companyDal.AddAsync(entity);
            return new SuccessResult();
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            await _companyDal.DeleteAsync(id);
            return new SuccessResult();
        }

        public async Task<IDataResult<List<Company>>> GetAllAsync(Expression<Func<Company,bool>>filter=null)
        {
            var data =await _companyDal.GetAllAsync(filter);
            return new SuccessDataResult<List<Company>>(data,"Mesaj");
        }

        public async Task<IDataResult<Company>> GetByIdAsync(int id)
        {
            var company = await _companyDal.GetAsync(x => x.Id == id);
            if (company!=null)
            {
                return new SuccessDataResult<Company>(company, "Mesaj");
            }
            return new ErrorDataResult<Company>(company,"Yok");
        }

        public async Task<IResult> UpdateAsync(Company entity)
        {
            await _companyDal.UpdateAsync(entity);
            return new SuccessResult();
        }
    }
}
