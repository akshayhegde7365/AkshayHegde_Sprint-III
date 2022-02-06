using Microsoft.AspNetCore.Mvc;
using Project_Management_API.Model;
using Project_Management_API.Repository;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var categories = _repository.GetUsers();
                if (categories == null)
                {
                    return NotFound();
                }

                return Ok(categories);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                var user = _repository.GetUser(id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post(User user)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var postId = _repository.AddUser(user);
                    if (postId > 0)
                    {
                        return Ok(postId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    return BadRequest();
                }

            }

            return BadRequest();
            //return _repository.AddUser(user);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.UpdateUser(id, user);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "System.InvalidOperationException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                int result = _repository.DeleteUser(id);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        // POST api/<UsersController>/login
        [HttpGet("/login/{username}")]
        public User Login(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                return _repository.GetUserByEmail(username);
            }
            return null;
        }
    }
}
