using leave_management.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace leave_management.Contracts
{
    public interface IContractRepository : IRepositoryBase<Contract>
    {
    }
}
