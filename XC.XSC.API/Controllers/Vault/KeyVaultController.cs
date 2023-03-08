using Microsoft.AspNetCore.Mvc;
using XC.CCMP.KeyVault.Manager;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XC.XSC.API.Controllers.Vault
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeyVaultController : ControllerBase
    {
        private readonly IKeyVaultManager _secretManager;

        public KeyVaultController(IKeyVaultManager secretManager)
        {
            _secretManager = secretManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string secretName)
        {
            try
            {
                if (string.IsNullOrEmpty(secretName))
                {
                    return BadRequest();
                }

                string secretValue = await _secretManager.GetSecret(secretName);

                if (!string.IsNullOrEmpty(secretValue))
                {
                    return Ok(secretValue);
                }
                else
                {
                    return NotFound("Secret key not found.");
                }
            }
            catch
            {
                return BadRequest("Error: Unable to read secret");
            }
        }
    }
}
