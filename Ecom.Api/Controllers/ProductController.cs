using AutoMapper;
using Ecom.Api.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var product = await _unitOfWork.ProductRepository.GetAllAsync(x => x.Category, x => x.Photos);
                if (product == null)
                {
                    return BadRequest(new ResponseApi(400));
                }
                var productDto = _mapper.Map<List<ProductDto>>(product);
                return Ok(productDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }
        [HttpGet("get-By-id/{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            try
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(id, x => x.Category, x => x.Photos);
                if (product == null)
                {
                    return BadRequest(new ResponseApi(400));
                }
                var productDto = _mapper.Map<ProductDto>(product);
                return Ok(productDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }
        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct(AddProductDto addProduct)
        {
            try
            {
                var res = await _unitOfWork.ProductRepository.AddAsync(addProduct);
                if (res)
                {
                    return Ok(new ResponseApi(200));
                }
                return BadRequest(new ResponseApi(400, "an error accure while saving product"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Update-product/{id}")]
        public async Task<IActionResult> UpadteProduct(int id, UpdateProductDto addProduct)
        {
            try
            {
                var res = await _unitOfWork.ProductRepository.UpdateAsync(id, addProduct);
                if (res)
                {
                    return Ok(new ResponseApi(200));
                }
                return BadRequest(new ResponseApi(400, "an error accure while upadting product"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var res = await _unitOfWork.ProductRepository.DeleteAsync(id);
                if (res)
                {
                    return Ok(new ResponseApi(200));
                }
                return BadRequest(new ResponseApi(400, "an error accure while deleting product"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
