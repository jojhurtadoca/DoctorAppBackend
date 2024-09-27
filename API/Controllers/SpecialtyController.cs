using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using System.Net;

namespace API.Controllers
{
    [Authorize(Policy = "AdminAgentPolicy")]
    public class SpecialtyController: BaseApiController
    {
        private readonly ISpecialtyService _specialtyService;
        private ApiResponse _response;
        public SpecialtyController(ISpecialtyService specialtyService)
        {
            _specialtyService = specialtyService;
            _response = new();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Result = await _specialtyService.GetAll();
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

        [HttpGet("get-actives")]
        public async Task<IActionResult> GetActives()
        {
            try
            {
                _response.Result = await _specialtyService.GetActives();
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
        public async Task<IActionResult> Create(SpecialtyDTO dto)
        {
            try
            {
                _response.Result = await _specialtyService.Add(dto);
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
        public async Task<IActionResult> Update(SpecialtyDTO dto)
        {
            try
            {
                await _specialtyService.Update(dto);
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
                await _specialtyService.Remove(id);
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
