using Glz.GreensJob.Dto;
using Glz.Infrastructure;
using Glz.GreensJob.Dto.RequestParams;

namespace Glz.GreensJob.Domain.IApplication
{
    /// <summary>
    /// 公司服务接口
    /// </summary>
    public interface ICompanyService : IApplicationServiceContract
    {
        CompanyObject GetObjectByID(int id);

        int PutCompany(CompanyActionRequestParam requestParam);

        int RemoveObject(int id);

        //int AddObject(CompanyObject obj);

        //int UpdateObject(CompanyObject obj);
    }
}
