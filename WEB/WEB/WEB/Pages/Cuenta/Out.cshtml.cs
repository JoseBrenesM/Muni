using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WEB.Pages.Cuenta
{
    public class OutModel : PageModel
    {
        
        public async Task<IActionResult> OnGet()
        {
            await HttpContext.SignOutAsync();

            return RedirectToPage("../Index");
        }
    }
}
