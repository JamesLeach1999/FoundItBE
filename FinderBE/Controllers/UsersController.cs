using FinderBE.Domain;
using FinderBE.Helpers;
using FinderBE.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;

namespace FinderBE.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController(IGetValues<User> userDb, ICreateValues<User, object> createUserValues, AbstractValidator<Guid> userIdValidator, AbstractValidator<User> userRequestValidator) : Controller
{

    /// <summary>
    /// Endpoint to get all users in the table
    /// </summary>
    /// <returns>List of all users</returns>
    /// <response code="200">When the call is successful</response>
    /// <response code="500">For any other error</response>
    [HttpGet]
    [Route("GetUsers")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        var allUsers = await userDb.GetValues();

        return Ok(allUsers);
    }

    /// <summary>
    /// Endpoint to get a single user based on their ID
    /// </summary>
    /// <returns>List of all users</returns>
    /// <response code="200">When the call is successful</response>
    /// <response code="400">When the Guid format is invalid</response>
    /// <response code="404">When no user with that ID is found</response>
    /// <response code="500">For any other error</response>
    [HttpGet]
    [Route("GetUser")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<User>> GetUser([FromQuery] Guid userId)
    {
        try
        {
            var validUserId = await userIdValidator.ValidateAsync(userId);

            if (!validUserId.IsValid)
            {
                return BadRequest("Invalid user id");
            }

            var user = await userDb.GetValue(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Create a user with an email, password and username
    /// </summary>
    /// <returns>Confirmation of a newly created user</returns>
    /// <response code="201">New user successfully created with introductory message</response>
    /// <response code="400">Validation error with user credentials</response>
    /// <response code="500">Any other internal error</response>
    [HttpPost]
    [Route("CreateUser")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<object>> CreateUser([FromBody] User user)
    {
        try
        {
            var validUser = await userRequestValidator.ValidateAsync(user);

            if (!validUser.IsValid)
            {
                return BadRequest("Invalid user credentials");
            }

            var creationResult = createUserValues.PostUser(user);

            if(creationResult == null)
            {
                return StatusCode(500);
            }

            return Ok(new { WelcomeMessage = $"Welcome {user.Username}" });
        }
        catch (Exception ex) {
            Console.WriteLine(ex);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// This is a simple endpoint used for testing
    /// </summary>
    /// <returns>Static user object</returns>
    /// <response code="200">When the call is successful</response>
    /// <response code="500">For any other error</response>
    [HttpGet]
    [Route("TestEndpoint")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<User>> Get()
    {
        var emailString = $"{CredentialsGenerator.GenerateRandomString(5)}@{CredentialsGenerator.GenerateRandomString(5)}.com";
        var passwordString = $"{CredentialsGenerator.GeneratePassword(8)}";
        var phone = "07777777777";
        var user = new { UserId = Guid.NewGuid(), Email = emailString, AccountCreatedDate = DateTime.Now, Password = passwordString, PhoneNumber = phone, Username = CredentialsGenerator.GenerateRandomString(6) };
        return Ok(user);
    }
}