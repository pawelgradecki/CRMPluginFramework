using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Odx.Crm.Core.Model;

namespace Odx.Crm.Core.DataAccess.Repositories.Logic
{
    internal class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(IOrganizationService service) : base(service) { }

        public Account GetAccountByAccountNumber(string accountNumber, Func<Account, Account> selector)
        {
            using (var ctx = new XrmServiceContext(this.service))
            {
                var result = ctx.AccountSet
                    .Where(x => x.AccountNumber == accountNumber)
                    .Select(x => selector(x)).ToList();
                return result.FirstOrDefault();

            }
        }

        public List<Account> GetAccountsByName(string accountName, Func<Account, Account> selector)
        {
            using (var ctx = new XrmServiceContext(this.service))
            {
                return ctx.AccountSet
                    .Where(x => x.Name == accountName)
                    .Select(x => selector(x)).ToList();

            }
        }

        public List<Account> GetActiveAccountsWithPaging(int skip, int take)
        {
            using (var ctx = new XrmServiceContext(this.service))
            {
                return ctx.AccountSet
                    .Where(x => x.StateCode == 0)
                    .Select(x => new Account { Id = x.Id })
                    .Skip(skip).Take(take).ToList();

            }
        }

        public List<Guid> GetSubaccountsListByParentAccountId(Guid parentAccountId)
        {
            using (var ctx = new XrmServiceContext(this.service))
            {
                return ctx.AccountSet
                    .Where(x => x.StateCode == AccountState.Active && x.ParentAccountId.Id == parentAccountId)
                    .Select(x => x.Id)
                    .ToList();
            }
        }
    }
}
