using AmazePay.Model;
using AmazePay.Repository;
using AmazePay.Security;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace AmazePay.Controller
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

        [HttpPost]
        [Route("/Login")]
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
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
    }
}
