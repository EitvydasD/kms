using Microsoft.AspNetCore.Mvc.Filters;
using System.Transactions;

namespace KMS.API.Middlewares;

public class TransactionPerRequestFilter : IActionFilter
{
    private TransactionScope? TransactionScope { get; set; }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        TransactionScope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (TransactionScope == null)
        {
            return;
        }

        if (context.Exception == null)
        {
            TransactionScope.Complete();
        }

        TransactionScope.Dispose();
    }
}
