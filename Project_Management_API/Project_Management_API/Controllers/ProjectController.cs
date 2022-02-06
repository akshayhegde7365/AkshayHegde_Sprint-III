using Microsoft.AspNetCore.Mvc;
using Project_Management_API.Model;
using Project_Management_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _repository;
        public ProjectController(IProjectRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<ProjectsController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var categories = _repository.GetProjects();
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

        // GET api/<ProjectsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                var Project = _repository.GetProject(id);

                if (Project == null)
                {
                    return NotFound();
                }

                return Ok(Project);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST api/<ProjectsController>
        [HttpPost]
        public IActionResult Post(Project Project)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var postId = _repository.AddProject(Project);
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
        }

        // PUT api/<ProjectsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Project Project)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.UpdateProject(id, Project);

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

        // DELETE api/<ProjectsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                int result = _repository.DeleteProject(id);
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
    }
}
