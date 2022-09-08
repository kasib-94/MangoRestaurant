using System.Collections;
using AutoMapper;
using Mango.Services.ProductAPI.DbContext;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Repository;

public class ProductRepository :  IProductRepository
{
    private readonly ApplicationDbContext _db;
    private IMapper _mapper;
    public ProductRepository(ApplicationDbContext db,IMapper mapper)
    {
        _mapper = mapper; 
        _db = db;
    }
    
    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        IEnumerable <Product> productList = await _db.Products.ToListAsync();
        return _mapper.Map<List<ProductDto>>(productList);

    }

    public async Task<ProductDto> GetProductById(int productId)
    {
      Product product = await _db.Products.Where(x=>x.ProductId==productId).FirstOrDefaultAsync();
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
    {
        Product product = _mapper.Map<ProductDto, Product>(productDto);
        if (product.ProductId>0)
        {
            _db.Products.Update(product);
        }
        else
        {
            _db.Products.Add(product);
        }

        await _db.SaveChangesAsync();
        return _mapper.Map<Product, ProductDto>(product);
    }

    public async Task<bool> DeleteProduct(int productId)
    {
        try
        {
            Product product = await _db.Products.Where(x => x.ProductId == productId).FirstOrDefaultAsync();
            if (product==null)
            {
                return false;
            }
          
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return true;
            
        }
        catch (Exception e)
        {
            return false;
        }
    }
}