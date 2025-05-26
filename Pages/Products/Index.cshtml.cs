// using dotnet_mvc.Controllers;
// using Microsoft.AspNetCore.Mvc.RazorPages;
// using ProductAPI.DTOs;
// using ProductAPI.Interfaces;
// using ProductAPI.Models;

// namespace ProductAPI.Pages.Products
// {
//     public class IndexModel : PageModel
//     {
//         private readonly IProductService _productService;

//         public PaginatedResult<GetProductDto> Products { get; set; }

//         public IndexModel(IProductService productService)
//         {
//             _productService = productService;
//         }

//         public async Task OnGetAsync()
//         {
//             Products = await _productService.GetAllProducts(1, 10, "", "asc");
//         }
//     }
// }



using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductAPI.DTOs;
using ProductAPI.Interfaces;

public class IndexModel : PageModel
{
    private readonly IProductService _productService;

    public List<GetProductDto> Products { get; set; } = new();
    public int TotalProducts { get; set; }
    public int UniqueProducts { get; set; }
    public int TotalVendors { get; set; }
    public int CartCount { get; set; }
    public string SearchText { get; set; } = "";
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalProducts / PageSize);
    public int TotalResults => TotalProducts;
    public int StartResult => (CurrentPage - 1) * PageSize + 1;
    public int EndResult => Math.Min(CurrentPage * PageSize, TotalProducts);

    public IndexModel(IProductService productService)
    {
        _productService = productService;
    }

    public async Task OnGetAsync(int pageNumber = 1, int pageSize = 8, string? search = "")
    {
        SearchText = search ?? "";
        CurrentPage = pageNumber;
        PageSize = pageSize;

        var result = await _productService.GetAllProducts(pageNumber, pageSize, search, null);
        Products = result.Items.ToList();
        TotalProducts = result.TotalCount;

        // In a real app you'd fetch these from services or counts in your DB:
        UniqueProducts = Products.Select(p => p.ProductName).Distinct().Count();
        TotalVendors = 8;     // placeholder
        CartCount = 0;        // you'd get from a cart service
    }
}
