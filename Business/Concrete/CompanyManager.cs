using Business.Abstract;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
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

        public IResult Add(Company entity)
        {
            _companyDal.Add(entity);
            return new SuccessResult();
        }

        public IResult Delete(int id)
        {
            _companyDal.Delete(id);
            return new SuccessResult();
        }

        public async Task<IDataResult<List<Company>>> GetAll()
        {
            var data =await _companyDal.GetAll();
            return new SuccessDataResult<List<Company>>(data,"Mesaj");
        }

        public IDataResult<Company> GetById(int id)
        {
            return new SuccessDataResult<Company>(_companyDal.Get(x => x.Id == id),"Mesaj");
        }

        public IResult Update(Company entity)
        {
            _companyDal.Update(entity);
            return new SuccessResult();
        }
    }
}
