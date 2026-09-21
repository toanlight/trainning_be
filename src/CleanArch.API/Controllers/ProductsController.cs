using CleanArch.Application.DTOs.Common;
using CleanArch.Application.DTOs.Product;
using CleanArch.Application.Interfaces.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.API.Controllers;

/// <summary>
/// Controller quản lý CRUD sản phẩm.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IValidator<CreateProductRequest> _createValidator;
    private readonly IValidator<UpdateProductRequest> _updateValidator;

    public ProductsController(
        IProductService productService,
        IValidator<CreateProductRequest> createValidator,
        IValidator<UpdateProductRequest> updateValidator)
    {
        _productService = productService;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    /// <summary>
    /// Lấy danh sách sản phẩm có phân trang và tìm kiếm.
    /// </summary>
    /// <param name="query">Tham số phân trang và tìm kiếm.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Danh sách sản phẩm theo trang.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<ProductDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] PagedQuery query, CancellationToken cancellationToken)
    {
        var result = await _productService.GetAllAsync(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Lấy thông tin sản phẩm theo Id.
    /// </summary>
    /// <param name="id">Id của sản phẩm (GUID).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Thông tin sản phẩm.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var product = await _productService.GetByIdAsync(id, cancellationToken);
        return Ok(product);
    }

    /// <summary>
    /// Tạo sản phẩm mới.
    /// </summary>
    /// <param name="request">Thông tin sản phẩm cần tạo.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Sản phẩm vừa được tạo.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var validation = await _createValidator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
            return BadRequest(validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        var product = await _productService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    /// <summary>
    /// Cập nhật thông tin sản phẩm.
    /// </summary>
    /// <param name="id">Id của sản phẩm cần cập nhật.</param>
    /// <param name="request">Thông tin cần cập nhật (partial update).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Sản phẩm sau khi cập nhật.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var validation = await _updateValidator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
            return BadRequest(validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        var product = await _productService.UpdateAsync(id, request, cancellationToken);
        return Ok(product);
    }

    /// <summary>
    /// Xóa mềm sản phẩm (Soft Delete — không xóa khỏi database).
    /// </summary>
    /// <param name="id">Id của sản phẩm cần xóa.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _productService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
