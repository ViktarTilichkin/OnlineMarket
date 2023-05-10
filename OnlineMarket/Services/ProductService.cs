using OnlineMarket.Models.Repository;
using OnlineMarket.Models.View;
using OnlineMarket.Repository;

namespace OnlineMarket.Services
{
    public class ProductService
    {
        private readonly ProductRepository m_Prod;
        private readonly CategoryRepository m_Catg;
        private readonly BrandRepository m_Brand;
        public ProductService(ProductRepository prod, CategoryRepository catg, BrandRepository brand)
        {
            m_Prod = prod;
            m_Catg = catg;
            m_Brand = brand;
        }
        public async Task<List<ViewProduct>> GetAll()
        {
            List<Product> prod = await m_Prod.GetAll();
            List<ViewProduct> product = new List<ViewProduct>();
            foreach (Product item in prod)
            {
                var viewProd = new ViewProduct();
                viewProd.Id = item.Id;
                viewProd.Name = item.Name;
                viewProd.Description = item.Description;
                viewProd.Price = item.Price;
                var resultCat = await m_Catg.GetById(item.Category_id);
                viewProd.Category = resultCat.Name;
                var resultBrand = await m_Brand.GetById(item.Brand_Id);
                viewProd.Brand = resultBrand.Name;
                product.Add(viewProd);
            }
            return product;
        }
        public async Task<ViewProduct> GetById(int id)
        {
            if (id <= 0) throw new ArgumentNullException(nameof(id));
            Product prod = await m_Prod.GetById(id);
            ViewProduct product = new ViewProduct();
            product.Id = prod.Id;
            product.Name = prod.Name;
            product.Description = prod.Description;
            product.Price = prod.Price;
            var resultCat = await m_Catg.GetById(prod.Category_id);
            product.Category = resultCat.Name;
            var resultBrand = await m_Brand.GetById(prod.Brand_Id);
            product.Brand = resultBrand.Name;
            return product;
        }
        public async Task<ViewProduct> Create()
        {
            return null;
        }
        public async Task<ViewProduct> Update()
        {
            return null;
        }
        public async Task<bool> Delete(int id)
        {
            return true;
        }
    }
}
