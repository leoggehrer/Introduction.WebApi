﻿using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Introduction.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // GET: api/<StudentsController>
        [HttpGet(Name = "Get")]
        public async Task<ActionResult<IEnumerable<Models.Student>>> GetAsync()
        {
            return await Data.StudentRepository.GetAllAsync();
        }

        // GET api/<StudentsController>/5
        [HttpGet("{id}", Name = "GetById")]
        [ProducesResponseType(typeof(Models.Student), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Models.Student), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Models.Student?>> GetAsync(int id)
        {
            var result = await Data.StudentRepository.GetByIdAsync(id);

            return result == null ? NotFound() : Ok(result);
        }

        // POST api/<StudentsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Models.Student>> PostAsync([FromBody] Models.Student model)
        {
            var insertModel = new Models.Student()
            {
                MatriculationNumber = model.MatriculationNumber,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
            };

            await Data.StudentRepository.InsertAsync(insertModel);
            await Data.StudentRepository.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = insertModel.Id }, insertModel);
        }

        // PUT api/<StudentsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Models.Student>> PutAsync(int id, [FromBody] Models.Student model)
        {
            var updateModel = await Data.StudentRepository.GetByIdAsync(id);

            if (updateModel != null)
            {
                updateModel.MatriculationNumber = model.MatriculationNumber;
                updateModel.Firstname = model.Firstname;
                updateModel.Lastname = model.Lastname;

                await Data.StudentRepository.UpdateAsync(updateModel);
                await Data.StudentRepository.SaveChangesAsync();
            }
            return updateModel == null ? NotFound() : Ok(updateModel);
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await Data.StudentRepository.GetByIdAsync(id);

            if (model != null)
            {
                await Data.StudentRepository.DeleteAsync(model.Id);
                await Data.StudentRepository.SaveChangesAsync();
            }
            return model == null ? NotFound() : NoContent();
        }
    }
}
