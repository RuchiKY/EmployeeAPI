using EmployeeAPI.DAL;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDBContext dbContext;

        public EmployeeController(EmployeeDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllUsers(string name)
        {
            try
            {
                if (name != null)
                {
                    var res = dbContext.Employee.Where(x => x.FirstName == name || x.LastName == name);
                    return Ok(res);
                }

                return Ok(dbContext.Employee);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeModel employee)
        {
            try
            {
                var emp = employee.BindModel();
                dbContext.Employee.Add(emp);
                await dbContext.SaveChangesAsync();
                return Ok("Created");
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
