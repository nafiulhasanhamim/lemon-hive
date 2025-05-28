using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;

public class IndexModel : PageModel
{
    private readonly IProductService _productService;
    private readonly ICartService _cartService;

    public List<GetProductDto> Products { get; set; } = new();
    public int TotalProducts { get; set; }
    public int TotalVendors { get; set; }
    public int CartCount { get; set; }
    public int? UniqueCount { get; set; }
    public string SearchText { get; set; } = "";
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalProducts / PageSize);
    public int TotalResults => TotalProducts;
    public int StartResult => (CurrentPage - 1) * PageSize + 1;
    public int EndResult => Math.Min(CurrentPage * PageSize, TotalProducts);

    public IndexModel(IProductService productService, ICartService cartService)
    {
        _productService = productService;
        _cartService = cartService;
    }


    public async Task<IActionResult> OnGetAsync(int pageNumber = 1, int pageSize = 8, string? search = "")
    {
        SearchText = search ?? "";
        CurrentPage = pageNumber;
        PageSize = pageSize;

        var result = await _productService.GetAllAsync(pageNumber, pageSize, search, null);
        Products = result.Items.ToList();
        TotalProducts = result.TotalCount;

        TotalVendors = 8;      
        CartCount = 0;      
        UniqueCount = result.UniqueCount;        

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return new JsonResult(new
            {
                items = Products,
                totalResults = TotalProducts,
                page = CurrentPage,
                pageSize = PageSize
            });
        }

        return Page();
    }

    public class AddToCartRequest
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string Message { get; set; } = "";
        public int StatusCode { get; set; }
    }

    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> OnPostAddToCartAsync([FromBody] AddToCartRequest req)
    {
        // Map to the service DTO
        var dto = new AddToCartDto
        {
            ProductId = req.ProductId,
            Quantity = req.Quantity
        };
        Console.WriteLine(dto.ProductId);

        // Call your cart‐service
        CartDetailDto? detail = null;
        try
        {
            detail = await _cartService.AddToCartAsync(dto);
        }
        catch (Exception ex)
        {
            // if your service throws on duplicates
            return new JsonResult(new ApiResponse<object>
            {
                Success = false,
                Data = null,
                Message = "Item already exists in the cart.",
                StatusCode = 400
            })
            { StatusCode = 400 };
        }

        // If the service returned null for any reason, consider it a failure
        if (detail == null)
        {
            return new JsonResult(new ApiResponse<object>
            {
                Success = false,
                Data = null,
                Message = "Unable to add item.",
                StatusCode = 500
            })
            { StatusCode = 500 };
        }

        // Otherwise wrap the detail in your ApiResponse
        return new JsonResult(new ApiResponse<CartDetailDto>
        {
            Success = true,
            Data = detail,
            Message = "Resource Created",
            StatusCode = 201
        })
        { StatusCode = 201 };
    }
}