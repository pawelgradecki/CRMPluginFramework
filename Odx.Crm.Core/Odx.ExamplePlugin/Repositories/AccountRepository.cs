using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Odx.Crm.Core.DataAccess;
using Odx.Crm.Core.Model;

namespace Odx.ExamplePlugin.Repositories
{
    internal class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(IOrganizationService service) : base(service)
        {
        }

        public Account GetAccountByAccountNumber(string accountNumber, Func<Account, Account> selector)
        {
            return this.CustomRetrieve(ctx =>
            {
                return ctx.AccountSet
                .Where(a => a.AccountNumber == accountNumber)
                .Select(selector)
                .ToList()
                .FirstOrDefault();
            });             
        }
    }
}
