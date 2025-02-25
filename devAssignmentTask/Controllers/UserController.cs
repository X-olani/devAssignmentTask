using devAssignmentTask.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace devAssignmentTask.Controllers
{

    [Route("api/users")]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    public class UserController : Controller
    {
        //congif the connecting string from appsetting to be used 
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        [HttpPost("create/")]
        public string CreateUser([FromBody] User user)
        {
           // Console.WriteLine($"FirstName: {user.firstName}, LastName: {user.lastName}, Age: {user.age}");

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("dbCon").ToString()))
            {
                string query = "InsertUser";

                using (SqlCommand comm = new SqlCommand(query, con))
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@f_Name", user.firstName);
                    comm.Parameters.AddWithValue("@l_Name", user.lastName);
                    comm.Parameters.AddWithValue("@dbirth", user.dateOfBirth);
                    comm.Parameters.AddWithValue("@age", user.age);
                    comm.Parameters.AddWithValue("@addr_1", user.address_1);
                    comm.Parameters.AddWithValue("@addr_2", user.address_2);
                    comm.Parameters.AddWithValue("@addr_3", user.address_3);
                    comm.Parameters.AddWithValue("@addr_4", user.address_4);
                    comm.Parameters.AddWithValue("@country", user.country);
                    comm.Parameters.AddWithValue("@lang", user.language);
                    comm.Parameters.AddWithValue("@bal", user.balance);
                    comm.Parameters.AddWithValue("@active", user.active);

                    //open the connection 
                    con.Open();
                    int i = comm.ExecuteNonQuery();

                    //close connection thro db
                    con.Close();

                    // sql comm will return a value if user was created in the db 
                    return i > 0 ? "User created" : "User not created";

                }
            }

        }



        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            //create array of object for users to be stored 
            List<User> allUsers = new List<User>();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("dbCon").ToString()))
            {
                //the string to hold storeed procedure name 
                string query = "Showusers";

                //sql command to run query
                using (SqlCommand comm = new SqlCommand(query, con))
                {
                    //select the command type of thd action to bec executed
                    comm.CommandType = CommandType.StoredProcedure;


                    con.Open();

                    //the command returns a list of rows 
                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        //loop through the rows and add the to user array
                        while (reader.Read())
                        {
                            //adding user into the array
                            allUsers.Add(new User
                            {
                                Id = reader.GetInt32(0),
                                firstName = reader.GetString(1),
                                lastName = reader.GetString(2),
                                dateOfBirth = reader.GetDateTime(3),
                                age = reader.GetString(4),
                                address_1 = reader.GetString(5),
                                address_2 = reader.GetString(6),
                                address_3 = reader.GetString(7),
                                address_4 = reader.GetString(8),
                                country = reader.GetString(9),
                                language = reader.GetString(10),
                                balance = reader.GetDecimal(11),
                                active = reader.GetString(12)[0],
                                created_date = reader.GetDateTime(13),


                            });
                        }
                    }

                  

                    return Ok(allUsers);

                }
            }
        }






        [HttpGet("getuser/{id}")]


        public async Task<IActionResult> GetUser(int id)
        {
            //
            User user = null;

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("dbCon").ToString()))
            {
                //the string to hold storeed procedure name 
                string query = "GetUser";

                using (SqlCommand comm = new SqlCommand(query, con))
                {
                    //stored procedure that take a parameter of id 
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@user_id", id);

                    con.Open();

                    //comm returns row for db and reader 
                    using (SqlDataReader reader = comm.ExecuteReader())
                    {

                       //check if reader is not contains data
                        if (reader.Read())
                        {
                            user = new User
                            {
                                Id = reader.GetInt32(0),
                                firstName = reader.GetString(1),
                                lastName = reader.GetString(2),
                                dateOfBirth=reader.GetDateTime(3),
                                age = reader.GetString(4),
                                address_1 = reader.GetString(5),
                                address_2 = reader.GetString(6),
                                address_3 = reader.GetString(7),
                                address_4 = reader.GetString(8),
                                country = reader.GetString(9),
                                language = reader.GetString(10),
                                balance = reader.GetDecimal(11),
                                active = reader.GetString(12)[0],
                                created_date = reader.GetDateTime(13),


                            };
                        }
                    }




                }
            }

            // check if user object is null or not and if not return user object
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound(new { message = "Not found" });
            }


        }


        [HttpPut("updateuser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("dbCon")))
            {
                //the string to hold storeed procedure name 
                string query = "UpdateUser";

                using (SqlCommand comm = new SqlCommand(query, con))
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@user_id", id);
                    comm.Parameters.AddWithValue("@f_Name", user.firstName);
                    comm.Parameters.AddWithValue("@l_Name", user.lastName);
                    comm.Parameters.AddWithValue("@dbirth", user.dateOfBirth);
                    comm.Parameters.AddWithValue("@age", user.age);
                    comm.Parameters.AddWithValue("@addr_1", user.address_1);
                    comm.Parameters.AddWithValue("@addr_2", user.address_2);
                    comm.Parameters.AddWithValue("@addr_3", user.address_3);
                    comm.Parameters.AddWithValue("@addr_4", user.address_4);
                    comm.Parameters.AddWithValue("@country", user.country);
                    comm.Parameters.AddWithValue("@bal", user.balance);
                    comm.Parameters.AddWithValue("@active", user.active);
                    comm.Parameters.AddWithValue("@lang", user.language);

                    //open db conection 
                    con.Open();

                    //onces command excuted returns value 
                    int row = await comm.ExecuteNonQueryAsync();

                    //if db row is affected row is 1
                    if (row > 0)
                    {
                        return Ok(new { message = "User was updated" });
                    }
                    else
                    {
                        return NotFound(new { message = "User not found" });
                    }

                }
            }
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("dbCon")))
            {
                //the string to hold storeed procedure name 
                string query = "DeleteUser";

                using (SqlCommand comm = new SqlCommand(query, con))
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@user_id", id);


                    //open db conection 
                    con.Open();

                    //onces command excuted returns value 
                    int row = await comm.ExecuteNonQueryAsync();

                    //if db row is affected row is 1
                    if (row > 0)
                    {
                        return Ok(new { message = "User was Deleted" });
                    }
                    else
                    {
                        return NotFound(new { message = "User not found" });
                    }

                }
            }
        }
    }
}
