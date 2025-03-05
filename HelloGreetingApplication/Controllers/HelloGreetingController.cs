using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using RepositoryLayer.Service;
using System.Threading.Tasks;

namespace HelloGreetingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloGreetingController : ControllerBase
    {
        private readonly IGreetingBL _greetingBL;

        public HelloGreetingController(IGreetingBL greetingBL)
        {
            _greetingBL = greetingBL;
        }

        /// <summary>
        /// Retrieves a welcome greeting from the API.
        /// </summary>
        /// <returns>A ResponseBody containing a welcome message and current date.</returns>
        [HttpGet]
        public IActionResult GetGreeting()
        {
            var greetingResult = _greetingBL.GetGreeting();
            var data = new
            {
                Greeting = greetingResult,
                Date = DateTime.Now
            };

            var response = new ResponseBody<object>
            {
                Success = true,
                Message = "Request successful",
                Data = data
            };
            return Ok(response);

        }



        /// <summary>
        /// Creates a personalized greeting based on provided user attributes.
        /// </summary>
        /// <param name="request">The RequestBody containing optional first name and last name.</param>
        /// <returns>A ResponseBody with a personalized greeting and creation timestamp.</returns>
        [HttpPost]
        public IActionResult Post([FromBody] RequestBody request)
        {
            var greetingResult = _greetingBL.GetGreeting(request.FirstName, request.LastName);
            var data = new
            {
                Greeting = greetingResult,
                Email = request.Email,
                ReceivedAt = DateTime.Now
            };

            var response = new ResponseBody<object>
            {
                Success = true,
                Message = "Greeting created",
                Data = data
            };
            return Ok(response);
        }





        /// <summary>
        /// Saves a new greeting message to the database and returns the created greeting with its auto-generated ID.
        /// </summary>
        /// <param >The greeting data provided by the client, containing the message to be saved.</param>
        /// <returns>An IActionResult with a 201 Created status, including a ResponseModel containing the saved greeting details.</returns>
        [HttpPost]
        [Route("save")]
        public IActionResult SaveGreeting([FromBody] GreetingModel greetingModel)
        {
            var result = _greetingBL.SaveGreeting(greetingModel);

            var response = new ResponseBody<object>
            {
                Success = true,
                Message = "Greeting created",
                Data = result
            };
            return Created("Greeting Created", response);

        }

        [HttpGet("GetGreetingById/{id}")]
        public IActionResult GetGreetingById(int id)
        {
            var response = new ResponseBody<GreetingModel>();
            try
            {

                var result = _greetingBL.GetGreetingById(id);
                if (result != null)
                {
                    response.Success = true;
                    response.Message = "Greeting Message Found";
                    response.Data = result;
                    return Ok(response);
                }
                response.Success = false;
                response.Message = "Greeting Message Not Found";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, response);
            }
        }

        /// <summary>
        /// Updates a greeting with new user information.
        /// </summary>
        /// <param name="request">The RequestBody containing updated first name, last name, and email.</param>
        /// <returns>A ResponseBody with the updated full name, email, and update timestamp.</returns>
        [HttpPut]
        public IActionResult Put([FromBody] RequestBody request)
        {
            var data = new
            {
                FullName = $"{request.FirstName} {request.LastName}",
                Email = request.Email,
                UpdatedAt = DateTime.Now
            };

            var response = new ResponseBody<object>
            {
                Success = true,
                Message = "Greeting updated",
                Data = data
            };
            return Ok(response);
        }

        /// <summary>
        /// Partially updates a greeting with the provided fields.
        /// </summary>
        /// <param name="request">The RequestBody with optional fields to update (first name, last name, or email).</param>
        /// <returns>A ResponseBody showing which fields were updated and the update timestamp.</returns>
        [HttpPatch]
        public IActionResult Patch([FromBody] RequestBody request)
        {
            var data = new
            {
                UpdatedFields = new
                {
                    FirstName = string.IsNullOrEmpty(request.FirstName) ? "Not updated" : request.FirstName,
                    LastName = string.IsNullOrEmpty(request.LastName) ? "Not updated" : request.LastName,
                    Email = string.IsNullOrEmpty(request.Email) ? "Not updated" : request.Email
                },
                UpdatedAt = DateTime.Now
            };

            var response = new ResponseBody<object>
            {
                Success = true,
                Message = "Greeting partially updated",
                Data = data
            };
            return Ok(response);
        }

        /// <summary>
        /// Deletes a greeting.
        /// </summary>
        /// <returns>A ResponseBody confirming deletion with a timestamp.</returns>
        [HttpDelete]
        public IActionResult Delete()
        {
            var data = new
            {
                DeletedAt = DateTime.Now
            };

            var response = new ResponseBody<object>
            {
                Success = true,
                Message = "Greeting deleted",
                Data = data
            };
            return Ok(response);
        }

        [HttpGet("GetAllGreetings")]
        public IActionResult GetAllGreetings()
        {
            ResponseBody<List<GreetingModel>> response = new ResponseBody<List<GreetingModel>>();
            try
            {
                var result = _greetingBL.GetAllGreetings();
                if (result != null && result.Count > 0)
                {
                    response.Success = true;
                    response.Message = "Greeting Messages Found";
                    response.Data = result;
                    return Ok(response);
                }
                response.Success = false;
                response.Message = "No Greeting Messages Found";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, response);
            }
        }

        [HttpPut("EditGreeting/{id}")]
        public IActionResult EditGreeting(int id, GreetingModel greetModel)
        {
            ResponseBody<GreetingModel> response = new ResponseBody<GreetingModel>();
            try
            {
                var result = _greetingBL.EditGreeting(id, greetModel);
                if (result != null)
                {
                    response.Success = true;
                    response.Message = "Greeting Message Updated Successfully";
                    response.Data = result;
                    return Ok(response);
                }
                response.Success = false;
                response.Message = "Greeting Message Not Found";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, response);
            }
        }

        [HttpDelete("DeleteGreeting/{id}")]

        public IActionResult DeleteGreeting(int id)
        {
            ResponseBody<GreetingModel> response = new ResponseBody<GreetingModel>();
            try
            {
                var result = _greetingBL.DeleteGreeting(id);
                if (result != null)
                {
                    response.Success = true;
                    response.Message = "Greeting Message Deleted Successfully";
                    response.Data = result;
                    return Ok(response);
                }
                response.Success = false;
                response.Message = "Greeting Message Not Found";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, response);
            }
        }
    }
}