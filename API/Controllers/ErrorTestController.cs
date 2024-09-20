using API.Errors;
using Data;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;

namespace API.Controllers
{
    public class ErrorTestController: BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public ErrorTestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("not-found")]
        public ActionResult NotFoundError()
        {
            return NotFound(new ApiErrorResponse(404));
        }

        [HttpGet("server-error")]
        public ActionResult ServerError()
        {
            return StatusCode(500, new ApiErrorResponse(500));
        }

        [HttpGet("bad-request")]
        public ActionResult BadRequestError()
        {
            return BadRequest(new ApiErrorResponse(400));
        }

        [HttpGet("unauthorized")]
        public ActionResult UnauthorizedError()
        {
            return Unauthorized(new ApiErrorResponse(401));
        }
    }
}
