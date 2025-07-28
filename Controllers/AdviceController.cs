using Microsoft.AspNetCore.Mvc;
using PatientAdviceAPI.Interfaces;

namespace PatientAdviceAPI.Controllers
{
    [ApiController]
    [Route("api/v1/patients")]
    public class AdviceController : ControllerBase
    {
        private readonly IAdviceService _adviceService;

        public AdviceController(IAdviceService adviceService)
        {
            _adviceService = adviceService;
        }

        [HttpGet("{patientId}/advice")]
        public async Task<IActionResult> GetAdvice(string patientId)
        {
            var advice = await _adviceService.GenerateAdviceAsync(patientId);
            return Ok(new { advice });
        }
    }
}
