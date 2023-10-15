using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.DTO;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController:ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UsersController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager=signInManager;

        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser(UserDTO model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);//400

                   
            }
            var user= new AppUser
            {
                FullName=model.FullName,
                UserName=model.UserName,
                Email=model.Email,
                DateAdded=DateTime.Now

            };
            var result=await _userManager.CreateAsync(user,model.Password);
            if(result.Succeeded)
            {
                return StatusCode(201);//oluşturuldu.
            }
            return BadRequest(result.Errors);
        }

        [HttpGet]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var user=await _userManager.FindByEmailAsync(model.Email);
            if(user is null)
            {
                return BadRequest(new {messagge="email hatası"});
            }
            var result=await _signInManager.CheckPasswordSignInAsync(user,model.Password,false);
            if(result.Succeeded)
            {
                return Ok(new {token="token"});
            }
            return Unauthorized();//yetkisizlik
        }




    }


}