using CleanArch.Application.DTOs.Product;
using FluentValidation;

namespace CleanArch.Application.Validators.Product;

/// <summary>
/// Validator cho CreateProductRequest sử dụng FluentValidation.
/// </summary>
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Tên sản phẩm không được để trống.")
            .MaximumLength(200).WithMessage("Tên sản phẩm không được vượt quá 200 ký tự.");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Mô tả không được vượt quá 1000 ký tự.")
            .When(x => x.Description is not null);

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Giá sản phẩm phải lớn hơn 0.");

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("Số lượng không được âm.");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Danh mục sản phẩm không được để trống.")
            .MaximumLength(100).WithMessage("Danh mục không được vượt quá 100 ký tự.");
    }
}
