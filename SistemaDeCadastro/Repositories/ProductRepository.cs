using Microsoft.EntityFrameworkCore;
using Serilog;
using SistemaDeCadastro.Data;
using SistemaDeCadastro.Mapper;
using SistemaDeCadastro.Models;
using SistemaDeCadastro.Models.ModelsDTO;
using SistemaDeCadastro.Repositories.Interfaces;
using SistemaDeCadastro.Validation;

namespace SistemaDeCadastro.Repositories
{
    public class ProductRepository : IProduct
    {

        private readonly AppDbContext _context;
        private readonly ProductMapper _mapper;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
            _mapper = new ProductMapper();
        }

        public async Task<ProductModel> Criar(RequestProductDTO requestProductDTO)
        {
            ProductValidation.Validate(requestProductDTO);

            var productModel = _mapper.MapToModel(requestProductDTO);
            await _context.Products.AddAsync(productModel);
            await _context.SaveChangesAsync();

            return productModel;

        }

        public async Task<bool> Delete(int id)
        {
            var productModel = await GetById(id);

            if (productModel == null)
            {
                Log.Warning("Tentativa de deletar produto que não existe", id);
                return false;
            }

            _context.Products.Remove(productModel);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<ProductModel> Editar(int id, RequestProductDTO requestProductDTO)
        {
            ProductModel productExisting = await GetById(id);

            if (productExisting == null)
            {
                Log.Error("O produto não existe");
                throw new ArgumentException(nameof(productExisting.Id));
            }
            ProductValidation.Validate(requestProductDTO);

            productExisting.Name = requestProductDTO.Name;
            productExisting.Price = requestProductDTO.Price;
            productExisting.Quantity = requestProductDTO.Quantity;
            productExisting.Validation = requestProductDTO.Validation;

            await _context.SaveChangesAsync();
            return productExisting;

        }

        public async Task<IEnumerable<ProductModel>> GetAll()
        {
            return await _context.Products
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProductModel> GetById(int id)
        {
            ProductModel productModelId = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
            if (productModelId == null)
            {
                Log.Warning("Produto não existente");
                throw new KeyNotFoundException($" Produto com o ID {id} não encontrado");
            }

            return productModelId;
        }
    }
}
