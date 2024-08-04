using AppointmentAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AppointmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //The login works, but it still wants a username which is strange. Testet in PostMan.
        [HttpPost]
        [Route("login")]
        public string login(Users users)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection").ToString());
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Users WHERE Email ='"+users.email+"'" +
                "AND PasswordHash = '"+users.password+"'", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            connection.Close();
            if (dt.Rows.Count > 0)
            {
                return "User Found";
            }
            else
            {
                return "User NOT Found";
            }
        }
    }
}
