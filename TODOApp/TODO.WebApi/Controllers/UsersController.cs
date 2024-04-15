using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TODO.WebApi.Models;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly AppDbcontext _context;

    public UsersController(AppDbcontext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        //_context.Users.Add(user);

        await _context.AddAsync(user);
        await _context.SaveChangesAsync();

        return Ok();
    }



    [HttpGet("users-with-books")]
    public async Task<ActionResult<IEnumerable<User>>> GetUsersWithBooks()
    {
        try
        {

            var usersWithBooks = await _context.Users
                .Include(u => u.UsersBooks)
                .ToListAsync();


            var result = usersWithBooks.Select(u => new
            {
                UserId = u.UserId,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Password = u.Password,
                Books = u.UsersBooks.Select(b => new
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Author = b.Author,
                    PublicationYear = b.PublicationYear,
                    Rating = b.Rating,
                    Review = b.Review
                })
            });

            return Ok(result);
        }
        catch (Exception ex)
        {

            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, User user)
    {
        if (id != user.UserId)
        {
            return BadRequest();
        }

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserExists(int id)
    {
        return _context.Users.Any(e => e.UserId == id);
    }


}

