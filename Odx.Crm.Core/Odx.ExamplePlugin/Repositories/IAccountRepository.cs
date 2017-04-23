using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Odx.Crm.Core.DataAccess;
using Odx.Crm.Core.Model;

namespace Odx.ExamplePlugin.Repositories
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        Account GetAccountByAccountNumber(string accountNumber, Func<Account, Account> selector);
    }
}
