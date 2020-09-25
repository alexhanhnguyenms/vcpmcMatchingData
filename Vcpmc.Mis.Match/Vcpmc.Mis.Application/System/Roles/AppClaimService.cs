using AutoMapper;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.Data.Entities.Mongo;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.System.Roles;
using MongoDB.Driver.Linq;
using Vcpmc.Mis.Data.Entities.System;

namespace Vcpmc.Mis.Application.System.Roles
{
    public class AppClaimService: IAppClaimService
    {
        private readonly IMongoCollection<AppClaim> _collection;

        //private readonly IConfiguration _config;
        public AppClaimService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<AppClaim>(settings.AppClaimsCollectionName);           
        }

        #region GetData
        public async Task<PagedResult<AppClaimViewModel>> GetAllPaging(IMapper _mapper)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<AppClaim>()
                         select e);
            //3. Paging
            int totalRow = 0;
            var list = query.ToList();
            //4.map
            List<AppClaimViewModel> models = new List<AppClaimViewModel>();
            if (list != null && list.Count > 0)
            {
                totalRow = list.Count;
                for (int i = 0; i < list.Count; i++)
                {
                    AppClaimViewModel preVM = _mapper.Map<AppClaimViewModel>(list[i]);
                    //preVM.SerialNo = (request.PageIndex - 1) * request.PageSize + (i + 1);
                    models.Add(preVM);
                }
            }
            //4. Select and projection               
            var pagedResult = new PagedResult<AppClaimViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = list.Count == 0 ? 5000 : list.Count,
                PageIndex = 1,
                Items = models,
            };
            return pagedResult;
        }        
        #endregion
    }
}
