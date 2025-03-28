using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;

namespace Ecom.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly IImageProfileManagement _imageProfile;
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context, IMapper mapper, IImageProfileManagement imageProfile) : base(context)
        {
            _mapper = mapper;
            _imageProfile = imageProfile;
            _context = context;
        }

        public async Task<bool> AddAsync(AddProductDto productDto)
        {
            if (productDto == null) return false;
            var product = _mapper.Map<Product>(productDto);
            await AddAsync(product);
            var paths = await _imageProfile.AddImageAsync(productDto.Photo, productDto.Name);
            var photo = paths.Select(path => new Photo() { ImageName = path, ProductId = product.Id }).ToList();
            await _context.AddRangeAsync(photo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id, x => x.Photos, x => x.Category);
            if (product == null) return false;
            var photos = _context.Photos.Where(x => x.ProductId == id).ToList();
            foreach (var item in photos)
            {
                _imageProfile.DeleteImageAsync(item.ImageName);
            }
            await DeleteAsync(product);
            return true;
        }

        public async Task<bool> UpdateAsync(int id, UpdateProductDto productDto)
        {
            if (productDto == null) return false;
            var product = await GetByIdAsync(id, x => x.Photos, x => x.Category);
            if (product == null) return false;
            var mappedProduct = _mapper.Map(productDto, product);
            await UpdateAsync(mappedProduct);
            if (productDto.Photo != null)
            {
                var oldPhotos = _context.Photos.Where(x => x.ProductId == id).ToList();
                foreach (var item in oldPhotos)
                {
                    _imageProfile.DeleteImageAsync(item.ImageName);
                }
                _context.Photos.RemoveRange(oldPhotos);
                var paths = await _imageProfile.AddImageAsync(productDto.Photo, productDto.Name);
                var photo = paths.Select(path => new Photo() { ImageName = path, ProductId = product.Id }).ToList();
                await _context.AddRangeAsync(photo);
                await _context.SaveChangesAsync();
            }
            return true;
        }

    }
}
