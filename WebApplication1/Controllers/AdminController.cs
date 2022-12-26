using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        private readonly ShoppingContext _context;
        public AdminController(ShoppingContext shoppingContext)
        {
            _context = shoppingContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddressDetail()
        {
            List<AddressDetail> addressDetail = _context.AddressDetail.Include(user => user.User).Include(city => city.City).ToList();
            return View(addressDetail);
        }

        public IActionResult ViewAddressDetail(int id)
        {
            AddressDetail? addressDetail = _context.AddressDetail.Include(user => user.User).Include(city => city.City).Where(x => x.Id == id).FirstOrDefault();
            return View(addressDetail);
        }
        public IActionResult DeleteAddressDetail(int id)
        {
            AddressDetail? addressDetail = _context.AddressDetail?.Where(x => x.Id == id).FirstOrDefault();
            _context.AddressDetail?.Remove(addressDetail);
            _context.SaveChanges();
            return Redirect("/Admin/AddressDetail");
        }
        [HttpGet]
        public IActionResult AddOrUpdateAddressDetail(int id)
        {
            ViewBag.City = new SelectList(_context.City.ToList(),"Id", "Name");
            ViewBag.User = new SelectList(_context.User.ToList(), "Id", "Name");

            if (id == 0)
            {
                return View();
            }
            else
            {
                AddressDetail? addressDetail = _context.AddressDetail.Include(user => user.User).Include(city => city.City).Where(x => x.Id == id).FirstOrDefault(); ;
                return View(addressDetail);
            }

        }
        [HttpPost]
        public IActionResult AddOrUpdateAddressDetail(AddressDetail adressDetail)
        {
            _context.AddressDetail.Update(adressDetail);
            _context.SaveChanges();
            return Redirect("/Admin/AddressDetail");
        }

        [Admin]
        public IActionResult Category()
        {
            List<Category> Category = _context.Category.ToList();
            return View(Category);
        }

        [Admin]
        public IActionResult ViewCategory(int id)
        {
            Category? category = _context.Category.Where(x => x.Id == id).FirstOrDefault();
            return View(category);
        }
        public IActionResult DeleteCategory(int id)
        {
            Category? category = _context?.Category?.Where(x => x.Id == id).FirstOrDefault();
            _context.Category.Remove(category);
            _context.SaveChanges();
            return Redirect("/Admin/Category");
        }

        [Admin]
        [HttpGet]
        public IActionResult AddOrUpdateCategory(int id)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                Category category = _context.Category.Where(x => x.Id == id).FirstOrDefault();
                return View(category);
            }

        }
        
        [Admin]
        [HttpPost]
        public IActionResult AddOrUpdateCategory(Category category)
        {
            _context.Category.Update(category);
            _context.SaveChanges();
            return Redirect("/Admin/Category");
        }
        
        [Admin]
        public IActionResult SubCategory()
        {
            List<SubCategory> SubCategory = _context.SubCategory.Include(Category => Category.Category).ToList();
            return View(SubCategory);
        }
        [Admin]
        public IActionResult ViewSubCategory(int id)
        {
            SubCategory subCategory = _context?.SubCategory?.Include(Category => Category.Category).Where(x => x.Id == id).FirstOrDefault();
            return View(subCategory);
        }
        [Admin]
        public IActionResult DeleteSubCategory(int id)
        {

            //List<Category> Category = _context?.Category?.Where(x => x.Id == id).ToList();
            //foreach (var item in Category)
            //{
            //    _context.Category.Remove(item);
            //}

            //_context.SaveChanges();


            SubCategory? subCategory = _context?.SubCategory?.Where(x => x.Id == id).FirstOrDefault();
            _context.SubCategory.Remove(subCategory);
            _context.SaveChanges();
            return Redirect("/Admin/SubCategory");
        }
        
        [Admin]
        [HttpGet]
        public IActionResult AddOrUpdateSubCategory(int id)
        {
            ViewBag.Category = new SelectList(_context.Category.ToList(), "Id", "Name");

            if (id == 0)
            {
                return View();
            }
            else
            {
                SubCategory subCategory = _context.SubCategory.Where(x => x.Id == id).FirstOrDefault();
                return View(subCategory);
            }

        }
        [Admin]
        [HttpPost]
        public IActionResult AddOrUpdateSubCategory(SubCategory subCategory)
        {
            _context.SubCategory.Update(subCategory);
            _context.SaveChanges();
            return Redirect("/Admin/SubCategory");
        }
        [Admin]
        public IActionResult City()
        {
            List<City> City = _context.City.ToList();
            return View(City);
        }
        [Admin]
        public IActionResult ViewCity(int id)
        {
            City city = _context?.City.Where(x => x.Id == id).FirstOrDefault();
            return View(city);
        }
        [Admin]
        public IActionResult DeleteCity(int id)
        {
            List<AddressDetail> addressDetail = _context.AddressDetail?.Where(x => x.CityId == id).ToList();
            if (addressDetail is not null)
            {
                foreach (var item in addressDetail)
                {
                    _context.AddressDetail?.Remove(item);

                }
                _context.SaveChanges();
            }

            City city = _context?.City?.Where(x => x.Id == id).FirstOrDefault();
            _context.City.Remove(city);
            _context.SaveChanges();
            return Redirect("/Admin/City");
        }
        [HttpGet]
        [Admin]
        public IActionResult AddOrUpdateCity(int id)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                City city = _context.City.Where(x => x.Id == id).FirstOrDefault();
                return View(city);
            }

        }
        [Admin]
        [HttpPost]
        public IActionResult AddOrUpdateCity(City city)
        {
            _context.City.Update(city);
            _context.SaveChanges();
            return Redirect("/Admin/City");
        }
       
        [Seller]
        public IActionResult Image()
        {
            List<Images> Images = _context.Images.Include(Images => Images.Product).ToList();
            return View(Images);
        }
       
        [Seller]
        public IActionResult ViewImage(int id)
        {
            Images? Images = _context.Images?.Include(Images => Images.Product).Where(x => x.Id == id).FirstOrDefault();
            return View(Images);
        }
        
        [Seller]
        public IActionResult DeleteImage(int id)
        {
            Images? Images = _context.Images.Where(x => x.Id == id).FirstOrDefault();
            _context.Images.Remove(Images);
            _context.SaveChanges();
            return Redirect("/Admin/Image");
        }
        
        [Seller]
        [HttpGet]
        public IActionResult AddOrUpdateImage(int id)
        {
            ViewBag.Product = new SelectList(_context.Product.ToList(), "Id", "Title");
            if (id == 0)
            {
                return View();
            }
            else
            {
                Images Images = _context.Images.Where(x => x.Id == id).FirstOrDefault();
                return View(Images);
            }

        }
        [Seller]
        [HttpPost]
        public IActionResult AddOrUpdateImage(Images Images)
        {
            _context.Images.Update(Images);
            _context.SaveChanges();
            return Redirect("/Admin/Image");
        }
        [Admin]
        [Seller]
        [Buyer]
        public IActionResult Order()
        {
            List<Order> Order = _context.Order.Include(buyer => buyer.Buyer).Include(product => product.Product).Include(x => x.OrderStatus).ToList();

            return View(Order);
        }

        [HttpGet]
        public IActionResult AddOrUpdateOrder(int id = 0)
        {
            ViewBag.Product = new SelectList(_context.Product.ToList(), "Id", "Title");
            ViewBag.Buyer = new SelectList(_context.User.ToList(), "Id", "Name");
            ViewBag.OrderStatus = new SelectList(_context.OrderStatus.ToList(), "Id", "Name");

            if (id == 0)
            {
                return View();
            }
            else
            {
                Order Order = _context.Order.Include(buyer => buyer.Buyer).Include(product => product.Product).Include(x => x.OrderStatus).Where(x => x.Id == id).FirstOrDefault();

                return View(Order);
            }

        }
        [Seller]
        [Buyer]
        [HttpPost]
        public IActionResult AddOrUpdateOrder(Order order)
        {
            _context.Order.Update(order);
            _context.SaveChanges();
            return Redirect("/Admin/Order");
        }
        [Buyer]
        public IActionResult ViewOrder(int id)
        {
            Order order = _context.Order.Include(buyer => buyer.Buyer).Include(product => product.Product).Include(x => x.OrderStatus).Where(x => x.Id == id).FirstOrDefault();
            return View(order);
        }
        [Seller]
        [Buyer]
        public IActionResult DeleteOrder(int id)
        {
            Order order = _context.Order.Include(buyer => buyer.Buyer).Include(product => product.Product).Include(x => x.OrderStatus).Where(x => x.Id == id).FirstOrDefault();
            _context.Order.Remove(order);
            _context.SaveChanges();
            return Redirect("/Admin/Order");
        }
       
        [Seller]
        [Buyer]
        public IActionResult OrderStatus()
        {
            List<OrderStatus> orderStatus = _context.OrderStatus.ToList();
            return View(orderStatus);
        }
        [Buyer]
        [Seller]
        public IActionResult ViewOrderStatus(int id)
        {
            OrderStatus orderStatus = _context?.OrderStatus.Where(x => x.Id == id).FirstOrDefault();
            return View(orderStatus);
        }
        
        [Seller]
        public IActionResult DeleteOrderStatus(int id)
        {

            List<Order> order = _context?.Order?.Where(x => x.OrderStatusId == id).ToList();

            foreach (var item in order)
            {
                _context.Order.Remove(item);
            }
            OrderStatus orderStatus = _context?.OrderStatus?.Where(x => x.Id == id).FirstOrDefault();
            _context.OrderStatus.Remove(orderStatus);
            _context.SaveChanges();
            return Redirect("/Admin/OrderStatus");
        }
        [Seller]
        [HttpGet]
        public IActionResult AddOrUpdateOrderStatus(int id)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                OrderStatus orderStatus = _context.OrderStatus.Where(x => x.Id == id).FirstOrDefault();
                return View(orderStatus);
            }

        }
        [Seller]
        [HttpPost]
        public IActionResult AddOrUpdateOrderStatus(OrderStatus orderStatus)
        {
            _context.OrderStatus.Update(orderStatus);
            _context.SaveChanges();
            return Redirect("/Admin/OrderStatus");
        }

        [Admin]
        public IActionResult Product()
        {
            List<Product> Product = _context.Product.Include(seller => seller.Seller).Include(productStatus => productStatus.ProductStatus).Include(subCategory => subCategory.SubCategory).ToList();
            return View(Product);
        }
        [Admin]
        public IActionResult ViewProduct(int id)
        {
            Product product = _context.Product.Include(seller => seller.Seller).Include(productStatus => productStatus.ProductStatus).Include(subCategory => subCategory.SubCategory).Where(product => product.Id == id).FirstOrDefault();
            return View(product);
        }
        [Admin]
        public IActionResult DeleteProduct(int id)
        {
            List<Images> images = _context.Images?.Where(x => x.ProductId == id).ToList();
            if (images is not null)
            {
                foreach (var item in images)
                {
                    _context.Images?.Remove(item);
                    _context.SaveChanges();
                }
            }

            List<Order> order = _context.Order?.Where(x => x.ProductId == id).ToList();
            if (order is not null)
            {
                foreach (var item in order)
                {
                    _context.Order?.Remove(item);
                    _context.SaveChanges();
                }
            }

            Product product = _context.Product.Where(product => product.Id == id).FirstOrDefault();
            _context.Product.Remove(product);
            _context.SaveChanges();
            return Redirect("/Admin/Product");
        }
        [Admin]
        [HttpGet]
        public IActionResult AddOrUpdateProduct(int id)
        {
            ViewBag.ProductStatus = new SelectList(_context.ProductStatus.ToList(), "Id", "Name");
            ViewBag.SubCategory = new SelectList(_context.SubCategory.ToList(), "Id", "Name");
            ViewBag.Seller = new SelectList(_context.User.ToList(), "Id", "Name");

            if (id == 0)
            {
                return View();
            }
            else
            {
                Product product = _context.Product.Where(x => x.Id == id).FirstOrDefault();
                return View(product);
            }

        }
        [Admin]
        [HttpPost]
       
        public IActionResult AddOrUpdateProduct(Product product)
        {
            _context.Product.Update(product);
            _context.SaveChanges();
            return Redirect("/Admin/Product");
        }
        [Admin]
        public IActionResult ProductStatus()
        {
            List<ProductStatus> productStatus = _context.ProductStatus.ToList();
            return View(productStatus);
        }

        [Admin]
        public IActionResult ViewProductStatus(int id)
        {
            ProductStatus productStatus = _context?.ProductStatus.Where(x => x.Id == id).FirstOrDefault();
            return View(productStatus);
        }
        [Admin]
        public IActionResult DeleteProductStatus(int id)
        {
            List<Product> product = _context?.Product?.Where(x => x.ProductStatusId == id).ToList();

            foreach (var item in product)
            {
                _context.Product.Remove(item);
            }


            ProductStatus productStatus = _context?.ProductStatus?.Where(x => x.Id == id).FirstOrDefault();
            _context.ProductStatus.Remove(productStatus);
            _context.SaveChanges();
            return Redirect("/Admin/ProductStatus");
        }
       
        [Admin]
        [HttpGet]
        public IActionResult AddOrUpdateProductStatus(int id)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                ProductStatus productStatus = _context.ProductStatus.Where(x => x.Id == id).FirstOrDefault();
                return View(productStatus);
            }

        }
        [Admin]
        [HttpPost]
        public IActionResult AddOrUpdateProductStatus(ProductStatus productStatus)
        {
            _context.ProductStatus.Update(productStatus);
            _context.SaveChanges();
            return Redirect("/Admin/ProductStatus");
        }
        public IActionResult Role()
        {
            List<Role> Role = _context.Role.ToList();
            return View(Role);
        }
        [Admin]
        public IActionResult ViewRole(int id)
        {
            Role role = _context?.Role.Where(x => x.Id == id).FirstOrDefault();
            return View(role);
        }
        [Admin]
        public IActionResult DeleteRole(int id)
        {

            List<AddressDetail> addressDetails = _context?.AddressDetail?.Where(x => x.UserId == id).ToList();

            foreach (var item in addressDetails)
            {
                _context.AddressDetail.Remove(item);
            }

            List<Order> order = _context?.Order?.Where(x => x.BuyerId == id).ToList();

            foreach (var item in order)
            {
                _context.Order.Remove(item);
            }

            _context.SaveChanges();

            List<Product> products = _context?.Product?.Where(x => x.SellerId == id).ToList();

            foreach (var item in products)
            {
                _context.Product.Remove(item);
            }

            _context.SaveChanges();


            List<User> users = _context?.User?.Where(x => x.RoleId == id).ToList();

            foreach (var user in users)
            {
                _context.User.Remove(user);
            }
            
            _context.SaveChanges();


            Role role = _context?.Role?.Where(x => x.Id == id).FirstOrDefault();
            _context.Role.Remove(role);
            _context.SaveChanges();
            return Redirect("/Admin/Role");
        }
        [Admin]
        [HttpGet]
        public IActionResult AddOrUpdateRole(int id)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                Role role = _context.Role.Where(x => x.Id == id).FirstOrDefault();
                return View(role);
            }

        }
        
        [Admin]
        [HttpPost]
        public IActionResult AddOrUpdateRole(Role role)
        {
            _context.Role.Update(role);
            _context.SaveChanges();
            return Redirect("/Admin/Role");
        }
        [Admin]
        public IActionResult User()
        {
            List<User> User = _context.User.Include(user => user.Role).ToList();
            return View(User);
        }

        public IActionResult ViewUser(int id)
        {
            User user = _context.User.Include(user => user.Role).Where(user => user.Id == id).FirstOrDefault();
            return View(user);
        }

        [Admin]
        public IActionResult DeleteUser(int id)
        {
            AddressDetail addressDetail = _context.AddressDetail?.Where(x => x.UserId == id).FirstOrDefault();
            if (addressDetail is not null)
            {
                _context.AddressDetail?.Remove(addressDetail);
                _context.SaveChanges();
            }

            List<Images> images = _context.Images?.Where(x => x.ProductId == id).ToList();
            if (images is not null)
            {
                foreach (var item in images)
                {
                    _context.Images?.Remove(item);
                    _context.SaveChanges();
                }
            }

            Product product = _context.Product?.Where(x => x.SellerId == id).FirstOrDefault();
            if (product is not null)
            {
                _context.Product?.Remove(product);
                _context.SaveChanges();
            }

            User user = _context.User.Where(user => user.Id == id).FirstOrDefault();
            _context.User.Remove(user);
            _context.SaveChanges();
            return Redirect("/Admin/User");
        }
        [Admin]
        [HttpPost]
        public IActionResult AddOrUpdateUser(User user)
        {
            user.AccessToken = Guid.NewGuid().ToString();
            _context.User.Update(user);
            _context.SaveChanges();
            return Redirect("/Admin/User");
        }
        [Admin]
        [HttpGet]
        public IActionResult AddOrUpdateUser(int id = 0)
        {

            ViewBag.Role = new SelectList(_context.Role.ToList(), "Id", "Name");

            if (id == 0)
            {
                return View();
            }
            else
            {
                User User = _context.User.Include(user => user.Role).Where(x => x.Id == id).FirstOrDefault();

                return View(User);
            }

        }
    }
}
