using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.Abp.GraphQL.Pages;

public class HomeModel : AbpPageModel
{
    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}