using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using System.Net;

namespace API.Controllers
{
    [Authorize(Policy = "AdminAgentPolicy")]
    public class DoctorController: BaseApiController
    {
        private readonly IDoctorService _doctorService;
        private ApiResponse _response;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
            _response = new();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Result = await _doctorService.GetAll();
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorDTO dto)
        {
            try
            {
                _response.Result = await _doctorService.Add(dto);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(DoctorDTO dto)
        {
            try
            {
                await _doctorService.Update(dto);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                await _doctorService.Remove(id);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }
    }
}
