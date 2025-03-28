using AutoMapper;
using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;

namespace Ecom.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageProfileManagement _imageProfile;
        public UnitOfWork(AppDbContext context, IMapper mapper, IImageProfileManagement imageProfile)
        {
            _context = context;
            _mapper = mapper;
            _imageProfile = imageProfile;
            CategoryRepository = new CategoryRepository(_context);
            ProductRepository = new ProductRepository(_context, _mapper, _imageProfile);
            PhotoRepository = new PhotoRepository(_context);
            _mapper = mapper;
        }

        public ICategoryRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IPhotoRepository PhotoRepository { get; }

    }
}
