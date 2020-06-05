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

namespace Park_WebApi_Udemy.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/nationalparks")]
    [ApiVersion("2.0")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ApiExplorerSettings(GroupName = "ParkyOpenAPISpecNP")]

    public class NationalParksV2Controller : ControllerBase
    {
        private readonly INationalParkRepository _npRepo;
        private readonly IMapper _mapper;

        public NationalParksV2Controller(INationalParkRepository npRepo, IMapper mapper)
        {
            this._npRepo = npRepo;
            this._mapper = mapper;
        }

        /// <summary>
        /// Get list of national parks.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<NationalParkDto>))]
        public IActionResult GetNationalParks()
        {
            var parks = _npRepo.GetNationalParks().FirstOrDefault();

            //var parksDto = new List<NationalParkDto>();

            //foreach (var obj in parks)
            //{
            //    parksDto.Add(_mapper.Map<NationalParkDto>(obj));
            //}
            return Ok(_mapper.Map<NationalParkDto>(parks));
        }
    }
}