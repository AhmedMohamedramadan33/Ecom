using AutoMapper;
using Ecom.Api.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetAllAsync();
                if (category == null)
                {
                    return BadRequest(new ResponseApi(400));
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return BadRequest(new ResponseApi(400));
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategory(CategoryDto categorydto)
        {
            try
            {
                var category = _mapper.Map<Category>(categorydto);
                await _unitOfWork.CategoryRepository.AddAsync(category);
                return Ok(new ResponseApi(200, "Category Added Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update-category/{id}")]
        public async Task<IActionResult> Upadtecategory(int id, UpdateCategoryDto updateCategoryDto)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return BadRequest(new ResponseApi(400));
                }
                var UpdatedCategory = _mapper.Map(updateCategoryDto, category);

                await _unitOfWork.CategoryRepository.UpdateAsync(UpdatedCategory);
                return Ok(new ResponseApi(200, "Category Updated Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }
        [HttpDelete("delete-category/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return BadRequest(new ResponseApi(400));
                }
                await _unitOfWork.CategoryRepository.DeleteAsync(category);
                return Ok(new ResponseApi(200, "Category Deleted Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("server-error")]
        public async Task<IActionResult> GetServerError()
        {
            var thing = await _unitOfWork.ProductRepository.GetByIdAsync(42);
            thing.Name = " ";
            return Ok(thing);
        }
    }
}
