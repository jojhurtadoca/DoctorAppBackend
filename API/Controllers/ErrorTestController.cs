using Data;

namespace API.Controllers
{
    public class ErrorTestController: BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public ErrorTestController(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
