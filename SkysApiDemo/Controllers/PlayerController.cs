using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SkysApiDemo.Data;

namespace SkysApiDemo.Controllers
{
    [ApiController]
    [EnableCors("AllowAll")]
    [Route("{useremail}/[controller]")]
    public class PlayerController : ControllerBase
    {

        private readonly ILogger<PlayerController> _logger;
        private readonly ApplicationDbContext _context;

        public PlayerController(ILogger<PlayerController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetPlayers")]
        public IEnumerable<PlayerListDTO> Get(string useremail)
        {
            return _context.Players.Where(r => r.UserEmail == useremail)
                .Select(r => new PlayerListDTO
                {
                    Age = r.Age,
                    Born = r.Born,
                    Id = r.Id,
                    Jersey = r.Jersey,
                    Namn = r.Name,
                });
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id, string useremail)
        {
            var player = _context.Players.Where(r => r.UserEmail == useremail &&
                                                     r.Id == id).Select(r => new PlayerListDTO
            {
                Age = r.Age,
                Born = r.Born,
                Id = r.Id,
                Jersey = r.Jersey,
                Namn = r.Name

            }).FirstOrDefault();
            ;
            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }


        [HttpPut("{id}")]
        public IActionResult Put(string useremail, int id, [FromBody]PlayerUpdateDTO model)
        {
            var player = _context.Players.FirstOrDefault(e => 
                e.UserEmail == useremail &&
                e.Id == id);
            if (player == null) return NotFound();
            player.Age = model.Age;
            player.Born = model.Born;
            player.Jersey = model.Jersey;
            player.Name = model.Namn;
            _context.SaveChanges();
            return NoContent();

        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PlayerListDTO> Create(string useremail, PlayerCreateDTO model)
        {
            var player = new Player
            {
                Age = model.Age,
                UserEmail = useremail,
                Born = model.Born,
                Jersey = model.Jersey,
                Name = model.Namn,
            };
            _context.Players.Add(player);
            _context.SaveChanges();
            int id = player.Id;

            var listModel = new PlayerListDTO();
            listModel.Id = id;
            listModel.Age = player.Age;
            listModel.Born = player.Born;
            listModel.Jersey = player.Jersey;
            listModel.Namn = player.Name;

            return CreatedAtAction(nameof(Get), new { id = player.Id }, listModel);
        }

    }
}