using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodtruck.Shared.Supplements;

public interface ISupplementService
{
    Task<SupplementResult.Index> GetIndexAsync(SupplementRequest.Index request);
    Task<SupplementDto.Detail> GetDetailAsync(int supplementId);
    Task<int> CreateAsync(SupplementDto.Mutate model);
    Task EditAsync(int supplementId, SupplementDto.Mutate model);
    Task DeleteAsync(int supplementId);
}
