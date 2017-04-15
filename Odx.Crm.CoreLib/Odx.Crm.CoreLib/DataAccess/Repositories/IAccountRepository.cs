using Odx.Crm.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odx.Crm.Core.DataAccess.Repositories
{
    interface IAccountRepository : IBaseRepository<Account>
    {
        Account GetAccountByAccountNumber(string accountNumber, Func<Account, Account> selector);
    }
}
