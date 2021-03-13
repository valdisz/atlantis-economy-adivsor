namespace atlantis {
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authentication;
    using atlantis.Persistence;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Claims;
    using System;
    using System.Linq;

    [AllowAnonymous]
    public class AccountController : ControllerBase {
        public AccountController(Database db, AccessControl accessControl) {
            this.db = db;
            this.accessControl = accessControl;
        }

        private readonly Database db;
        private readonly AccessControl accessControl;

        [HttpPost("/login")]
        public async Task<IActionResult> LoginAsync([FromForm] LoginModel model, [FromQuery] string returnUrl) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if (User.Identity.IsAuthenticated) {
                await HttpContext.SignOutAsync();
            }

            var user = await db.Users.SingleOrDefaultAsync(x => x.Email == model.Email);
            if (user == null) {
                return Unauthorized();
            }

            if (!accessControl.VerifyPassword(user.Salt, model.Password, user.Digest)) {
                return Unauthorized();
            }

            var roles = user.Roles.Select(x => new Claim(WellKnownClaimTypes.Role, x.Role));

            var identity = new ClaimsIdentity(new[] {
                new Claim(WellKnownClaimTypes.UserId, user.Id.ToString()),
                new Claim(WellKnownClaimTypes.Email, user.Email),
            }.Concat(roles), "local", null, WellKnownClaimTypes.Role);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);

            var returnTo = new Uri(returnUrl ?? Url.Content("~/"));
            return Redirect(returnTo.PathAndQuery);
        }
    }

    public class LoginModel {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
