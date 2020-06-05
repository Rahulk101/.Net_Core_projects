using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Park_WebApi_Udemy.Repository.IRepository;
using AutoMapper;
using Park_WebApi_Udemy.Models.Dtos;
using Park_WebApi_Udemy.Models;
using Microsoft.AspNetCore.Authorization;

namespace Park_WebApi_Udemy.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/nationalparks")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ApiExplorerSettings(GroupName = "ParkyOpenAPISpecNP")]

    public class NationalParksController : ControllerBase
    {
        private readonly INationalParkRepository _npRepo;
        private readonly IMapper _mapper;

        public NationalParksController(INationalParkRepository npRepo,IMapper mapper)
        {
            this._npRepo = npRepo;
            this._mapper = mapper;
        }

        /// <summary>
        /// Get list of national parks.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200,Type=typeof(List<NationalParkDto>))]
        public IActionResult GetNationalParks()
        {
            var parks = _npRepo.GetNationalParks();

            var parksDto = new List<NationalParkDto>();

            foreach (var obj in parks)
            {
                parksDto.Add(_mapper.Map<NationalParkDto>(obj));
            }
            return Ok(parksDto);
        }

        /// <summary>
        /// Get individual national park
        /// </summary>
        /// <param name="id"> The Id of the national Park </param>
        /// <returns></returns>
        [HttpGet("{id:int}",Name = "GetNationalPark")]
        [ProducesResponseType(200, Type = typeof(NationalParkDto))]
        [ProducesResponseType(404)]
        [Authorize]
        [ProducesDefaultResponseType]
        public IActionResult GetNationalPark(int id)
        {
            var park = _npRepo.GetNationalPark(id);

            if(park==null)
            {
                return NotFound();
            }

            var parkDto = _mapper.Map<NationalParkDto>(park);

            //var parkDto = new NationalParkDto()
            //{
            //    Created = park.Created,
            //    Established = park.Established,
            //    Id = park.Id,
            //    Name = park.Name,
            //    State = park.State
            //};
            return Ok(parkDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(NationalParkDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateNationalPark([FromBody] NationalParkDto nationalParkDto)
        {
            if(nationalParkDto==null)
            {
                return BadRequest(ModelState);
            }
            if(_npRepo.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "National Park already Exists!");
                return StatusCode(404, ModelState);
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nationalParkObj = _mapper.Map<NationalPark>(nationalParkDto);
            if(!_npRepo.CreateNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record!{nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetNationalPark",new {version=HttpContext.GetRequestedApiVersion().ToString(),
                                                        id= nationalParkObj.Id},nationalParkObj);
        }

        [HttpPatch("{id:int}",Name ="UpdateNationalPark")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateNationalPark(int id, [FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null || id!=nationalParkDto.Id)
            {
                return BadRequest(ModelState);
            }
            var nationalParkObj = _mapper.Map<NationalPark>(nationalParkDto);
            if (!_npRepo.UpdateNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went wrong while updating the record!{nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{id:int}",Name = "DeleteNationalPark")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteNationalPark(int id)
        {
            if(!_npRepo.NationalParkExists(id))
            {
                return NotFound();
            }
            var nationalParkObj = _npRepo.GetNationalPark(id);
            if (!_npRepo.DeleteNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went wrong while updating the record!{nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}