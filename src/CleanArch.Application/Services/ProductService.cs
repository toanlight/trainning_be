using AutoMapper;
using CleanArch.Application.DTOs.Common;
using CleanArch.Application.DTOs.Product;
using CleanArch.Application.Interfaces.Services;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Exceptions;
using CleanArch.Domain.Interfaces;

namespace CleanArch.Application.Services;

/// <summary>
/// Service implement các use cases quản lý sản phẩm.
/// </summary>
public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<ProductDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(nameof(Product), id);

        return _mapper.Map<ProductDto>(product);
    }

    /// <inheritdoc/>
    public async Task<PagedResult<ProductDto>> GetAllAsync(PagedQuery query, CancellationToken cancellationToken = default)
    {
        var products = await _unitOfWork.Products.GetAllAsync(cancellationToken);

        // Apply search filter
        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            var search = query.SearchTerm.ToLower();
            products = products.Where(p =>
                p.Name.ToLower().Contains(search) ||
                p.Category.ToLower().Contains(search) ||
                (p.Description != null && p.Description.ToLower().Contains(search)));
        }

        // Apply sorting
        products = (query.SortBy?.ToLower(), query.SortDescending) switch
        {
            ("name", false)     => products.OrderBy(p => p.Name),
            ("name", true)      => products.OrderByDescending(p => p.Name),
            ("price", false)    => products.OrderBy(p => p.Price),
            ("price", true)     => products.OrderByDescending(p => p.Price),
            ("quantity", false) => products.OrderBy(p => p.Quantity),
            ("quantity", true)  => products.OrderByDescending(p => p.Quantity),
            ("category", false) => products.OrderBy(p => p.Category),
            ("category", true)  => products.OrderByDescending(p => p.Category),
            _                   => products.OrderByDescending(p => p.CreatedAt)
        };

        var totalCount = products.Count();
        var pagedProducts = products
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize);

        return PagedResult<ProductDto>.Create(
            _mapper.Map<IEnumerable<ProductDto>>(pagedProducts),
            totalCount,
            query.PageNumber,
            query.PageSize);
    }

    /// <inheritdoc/>
    public async Task<ProductDto> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken = default)
    {
        // Kiểm tra tên đã tồn tại
        if (await _unitOfWork.Products.IsNameTakenAsync(request.Name, cancellationToken))
            throw new ConflictException(nameof(Product), nameof(Product.Name), request.Name);

        var product = _mapper.Map<Product>(request);

        await _unitOfWork.Products.AddAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProductDto>(product);
    }

    /// <inheritdoc/>
    public async Task<ProductDto> UpdateAsync(Guid id, UpdateProductRequest request, CancellationToken cancellationToken = default)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(nameof(Product), id);

        // Kiểm tra tên mới có bị trùng không (bỏ qua chính nó)
        if (request.Name is not null && request.Name != product.Name)
        {
            if (await _unitOfWork.Products.IsNameTakenAsync(request.Name, cancellationToken))
                throw new ConflictException(nameof(Product), nameof(Product.Name), request.Name);
        }

        // Partial update — chỉ cập nhật các field được cung cấp
        if (request.Name is not null)        product.Name = request.Name;
        if (request.Description is not null) product.Description = request.Description;
        if (request.Price.HasValue)          product.Price = request.Price.Value;
        if (request.Quantity.HasValue)       product.Quantity = request.Quantity.Value;
        if (request.Category is not null)    product.Category = request.Category;
        product.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProductDto>(product);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(nameof(Product), id);

        _unitOfWork.Products.SoftDelete(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
