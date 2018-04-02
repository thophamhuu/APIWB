using Nop.Api.Models.Responses;
using Nop.Core.Domain.Catalog;
using Nop.Services.Catalog;
using Nop.Services.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nop.Api.Controllers
{
    [Authorize]
    public class MobileController : ApiController
    {
        #region Fields

        private readonly IProductService _productService;
        private readonly IPictureService _pictureService;
        private readonly ICategoryService _categoryService;

        #endregion

        #region Ctor

        public MobileController(IProductService productService, IPictureService pictureService, ICategoryService categoryService)
        {
            this._productService = productService;
            this._pictureService = pictureService;
            this._categoryService = categoryService;
        }

        #endregion

        #region Method

        public IList<ProductModel> GetAllProductsMobile()
        {
            return _productService.GetAllProductsMobile().Select(s => new ProductModel {
                Id = s.Id,
                Name = s.Name,
                Price = s.Price,
                Image = _pictureService.GetPictureUrl(_pictureService.GetPicturesByProductId(s.Id, 1).FirstOrDefault(), 210, true)
            }).ToList();
        }

        public IList<ProductModel> GetProductByCategoryMobile(int categoryId)
        {
            return _productService.GetAllProductsByCategory(categoryId).Select(s => new ProductModel
            {
                Id = s.Id,
                Name = s.Name,
                Price = s.Price,
                Image = _pictureService.GetPictureUrl(_pictureService.GetPicturesByProductId(s.Id, 1).FirstOrDefault(), 210, true)
            }).ToList();
        }

        public ProductDetailModel GetProductDetailMobile(int productId)
        {
            var product = _productService.GetProductById(productId);
            var data = new ProductDetailModel();
            data.Id = product.Id;
            data.Name = product.Name;
            data.Price = product.Price;
            data.ShortDescription = product.ShortDescription;
            data.FullDescription = product.FullDescription;
            if(product.ProductPictures != null)
            {
                foreach(var item in product.ProductPictures)
                {
                    data.Image.Add(_pictureService.GetPictureUrl(_pictureService.GetPicturesByProductId(productId, 1).FirstOrDefault(), 210, true));
                }
            }
            return data;
        }

        public IList<CategoryModel> GetAllCategory()
        {
            //var category =
                return new List<CategoryModel>();
        }

        #endregion
    }
}
