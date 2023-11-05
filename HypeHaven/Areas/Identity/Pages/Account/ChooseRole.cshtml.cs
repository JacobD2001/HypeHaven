/*using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HypeHaven.Areas.Identity.Data;

namespace HypeHaven.Areas.Identity.Pages.Account
{
    public class ChooseRoleModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Role { get; set; }
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Get the selected role from Input.Role
                var selectedRole = Input.Role;

                // Check the selected role and add it to the user
                if (selectedRole == "vendor" || selectedRole == "customer")
                {
                    // Add the selected role to the user
                    // Example:
                   
                }

                // Redirect the user to the home page or a different page
                return RedirectToPage("/Index");
            }

            // If the model state is not valid, redisplay the page
            return Page();
        }
    }
}
*/