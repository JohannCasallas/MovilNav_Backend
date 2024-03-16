using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovilNav_Backend.Model;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Resources;

namespace MovilNav_Backend.Controllers
{
    [ApiController]
    [Route("usersMovilNav")]
    public class UsersController : ControllerBase
    {
        ResourceManager rm = new ResourceManager("MovilNav_Backend.Resources.String-en-US", typeof(UsersController).Assembly);


        private readonly UsersContext _context;

        public UsersController(UsersContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("usersConsult")]
        public ActionResult<IEnumerable<ApiResponse>> ConsultarEstudiante()
        {
            return _context.Users.ToList();
        }

        [HttpPost]
        [Route("usersLogin")]
        public IActionResult UsersLogin([FromBody] LoginRequest request)
        {
            ApiResponse<string> response = new ApiResponse<string>();

            var user = _context.Users.SingleOrDefault(u => u.UserName == request.UserName && u.Passaword == request.Password);
            if (user != null)
            {
                user.LastLogin = DateTime.Now;
                _context.SaveChanges();

                response.Success = true;
                response.Data = "Login successful";
                return Ok(response);
            }
            else
            {
                response.Success = false;
                response.ErrorMessage = "Invalid username or password";
                return Ok(response);
            }
        }

        [HttpPut]
        [Route("usersUpdate/{userId}")]
        public IActionResult UpdateUser(int userId, [FromBody] ApiResponse updatedUser)
        {
            var existingUser = _context.Users.Find(userId);
            if (existingUser == null)
            {
                return NotFound(rm.GetString("UserNotFound"));
            }

            existingUser.UserName = updatedUser.UserName;
            existingUser.Passaword = updatedUser.Passaword;
            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.Email = updatedUser.Email;
            existingUser.ProfileUser = updatedUser.ProfileUser;
            existingUser.AccountStatus = updatedUser.AccountStatus;

            _context.SaveChanges();
            return Ok(rm.GetString("UserUpdated"));
        }

        [HttpDelete]
        [Route("usersDelete/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            var existingUser = _context.Users.Find(userId);
            if (existingUser == null)
            {
                return NotFound(rm.GetString("UserNotFound"));
            }

            _context.Users.Remove(existingUser);
            _context.SaveChanges();

            return Ok(rm.GetString("UserDelete"));
        }

        [HttpPost]
        [Route("usersCreate")]
        public IActionResult CreateUser([FromBody] ApiResponse newUser)
        {
            newUser.RegistrationDate = DateTime.Now;
            newUser.AccountStatus = false;

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Ok(rm.GetString("UserCreate"));
        }
    }
}
