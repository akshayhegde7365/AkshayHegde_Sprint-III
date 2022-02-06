using Microsoft.AspNetCore.Mvc;
using System;
using Task = Project_Management_API.Model.Task;
using Project_Management_API.Repository;

// For more information on enabling Web API for empty Tasks, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _repository;
        public TaskController(ITaskRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<TasksController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var categories = _repository.GetTasks();
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

        // GET api/<TasksController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                var Task = _repository.GetTask(id);

                if (Task == null)
                {
                    return NotFound();
                }

                return Ok(Task);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST api/<TasksController>
        [HttpPost]
        public IActionResult Post(Task Task)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var postId = _repository.AddTask(Task);
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

        // PUT api/<TasksController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Task Task)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.UpdateTask(id, Task);

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

        // DELETE api/<TasksController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                int result = _repository.DeleteTask(id);
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
