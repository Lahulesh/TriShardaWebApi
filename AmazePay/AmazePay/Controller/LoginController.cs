using AmazePay.Model;
using AmazePay.Repository;
using AmazePay.Security;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;

namespace AmazePay.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    //[RoutePrefix("api/Test")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        //[Route("api/[controller]")]
        //[ApiController]
    
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("/Login")]
        //[EnableCors("AllowOrigin")]
        public string Login(Login login)
        {
            string msg = string.Empty;
            try
            {
                var keySize = 1024;
                var rsaCryptoServiceProvider = new RSACryptoServiceProvider(keySize);

                Logger logger = new Logger();
                DataAccessLayerSQL dataAccessLayerSQL = new DataAccessLayerSQL();

                Cryptography cryptography = new Cryptography();
                var encryptedPassword = cryptography.Encrypt(login.Password, rsaCryptoServiceProvider.ExportParameters(false));
                var decryptedPassword = cryptography.Decrypt(encryptedPassword, rsaCryptoServiceProvider.ExportParameters(true));

                dataAccessLayerSQL.ExecuteProcedureIntOutput("usp_Login", login);

                //string connectionString = "Data Source=LAPTOP-VVRGR0S0\\SQLEXPRESS02;Initial Catalog=AmazePay;Integrated Security=True;Trust Server Certificate=True";

                //using (SqlConnection connection = new SqlConnection(connectionString))
                //{
                //    connection.Open();
                //    SqlCommand cmd = new SqlCommand("usp_Login", connection);
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.Parameters.AddWithValue("@Username", login.Username);
                //    cmd.Parameters.AddWithValue("@Password", encryptedPassword);
                //    cmd.Parameters.AddWithValue("@Bankcode", login.Bankcode);
                //    int i = cmd.ExecuteNonQuery();
                //    if (i > 0)
                //    {
                //        //  msg = "Data Inserted";
                //        logger.WriteActivityToFile("Data Inserted");
                //    }
                //    else
                //    {
                //        //  msg = "Error For Inserted";
                //        logger.WriteErrorToFile("Error For Inserted");
                //    }

                //}
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;





        }
    }
}
