﻿using Microsoft.AspNetCore.Mvc;
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
            ViewBag.City = _context.City.ToList();
            ViewBag.User = _context.User.ToList();

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
        public IActionResult Category()
        {
            List<Category> Category = _context.Category.ToList();
            return View(Category);
        }
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

        [HttpPost]
        public IActionResult AddOrUpdateCategory(Category category)
        {
            _context.Category.Update(category);
            _context.SaveChanges();
            return Redirect("/Admin/Category");
        }
        public IActionResult SubCategory()
        {
            List<SubCategory> SubCategory = _context.SubCategory.Include(Category => Category.Category).ToList();
            return View(SubCategory);
        }

        public IActionResult ViewSubCategory(int id)
        {
            SubCategory subCategory = _context?.SubCategory?.Include(Category => Category.Category).Where(x => x.Id == id).FirstOrDefault();
            return View(subCategory);
        }
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
        [HttpGet]
        public IActionResult AddOrUpdateSubCategory(int id)
        {
            ViewBag.Category = _context.Category.ToList();

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

        [HttpPost]
        public IActionResult AddOrUpdateSubCategory(SubCategory subCategory)
        {
            _context.SubCategory.Update(subCategory);
            _context.SaveChanges();
            return Redirect("/Admin/SubCategory");
        }

        public IActionResult City()
        {
            List<City> City = _context.City.ToList();
            return View(City);
        }

        public IActionResult ViewCity(int id)
        {
            City city = _context?.City.Where(x => x.Id == id).FirstOrDefault();
            return View(city);
        }
        public IActionResult DeleteCity(int id)
        {
            City city = _context?.City?.Where(x => x.Id == id).FirstOrDefault();
            _context.City.Remove(city);
            _context.SaveChanges();
            return Redirect("/Admin/City");
        }
        [HttpGet]
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

        [HttpPost]
        public IActionResult AddOrUpdateCity(City city)
        {
            _context.City.Update(city);
            _context.SaveChanges();
            return Redirect("/Admin/City");
        }

        public IActionResult Image()
        {
            List<Images> Images = _context.Images.Include(Images => Images.Product).ToList();
            return View(Images);
        }

        public IActionResult ViewImage(int id)
        {
            Images? Images = _context.Images?.Include(Images => Images.Product).Where(x => x.Id == id).FirstOrDefault();
            return View(Images);
        }
        public IActionResult DeleteImage(int id)
        {
            Images? Images = _context.Images.Where(x => x.Id == id).FirstOrDefault();
            _context.Images.Remove(Images);
            _context.SaveChanges();
            return Redirect("/Admin/Image");
        }
        [HttpGet]
        public IActionResult AddOrUpdateImage(int id)
        {
            ViewBag.Product = _context.Product.ToList();
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

        [HttpPost]
        public IActionResult AddOrUpdateImage(Images Images)
        {
            _context.Images.Update(Images);
            _context.SaveChanges();
            return Redirect("/Admin/Images");
        }

        public IActionResult Order()
        {
            List<Order> Order = _context.Order.Include(buyer => buyer.Buyer).Include(product => product.Product).Include(x => x.OrderStatus).ToList();

            return View(Order);
        }

        [HttpGet]
        public IActionResult AddOrUpdateOrder(int id = 0)
        {
            ViewBag.Product = _context.Product.ToList();
            ViewBag.Buyer = _context.User.ToList();
            ViewBag.OrderStatus = _context.OrderStatus.ToList();

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

        [HttpPost]
        public IActionResult AddOrUpdateOrder(Order order)
        {
            _context.Order.Update(order);
            _context.SaveChanges();
            return Redirect("/Admin/Order");
        }
        public IActionResult ViewOrder(int id)
        {
            Order order = _context.Order.Include(buyer => buyer.Buyer).Include(product => product.Product).Include(x => x.OrderStatus).Where(x => x.Id == id).FirstOrDefault();
            return View(order);
        }

        public IActionResult DeleteOrder(int id)
        {
            Order order = _context.Order.Include(buyer => buyer.Buyer).Include(product => product.Product).Include(x => x.OrderStatus).Where(x => x.Id == id).FirstOrDefault();
            _context.Order.Remove(order);
            _context.SaveChanges();
            return Redirect("/Admin/Order");
        }
        public IActionResult OrderStatus()
        {
            List<OrderStatus> orderStatus = _context.OrderStatus.ToList();
            return View(orderStatus);
        }

        public IActionResult ViewOrderStatus(int id)
        {
            OrderStatus orderStatus = _context?.OrderStatus.Where(x => x.Id == id).FirstOrDefault();
            return View(orderStatus);
        }
        public IActionResult DeleteOrderStatus(int id)
        {
            OrderStatus orderStatus = _context?.OrderStatus?.Where(x => x.Id == id).FirstOrDefault();
            _context.OrderStatus.Remove(orderStatus);
            _context.SaveChanges();
            return Redirect("/Admin/OrderStatus");
        }
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

        [HttpPost]
        public IActionResult AddOrUpdateOrderStatus(OrderStatus orderStatus)
        {
            _context.OrderStatus.Update(orderStatus);
            _context.SaveChanges();
            return Redirect("/Admin/OrderStatus");
        }

        public IActionResult Product()
        {
            List<Product> Product = _context.Product.Include(seller => seller.Seller).Include(productStatus => productStatus.ProductStatus).Include(subCategory => subCategory.SubCategory).ToList();
            return View(Product);
        }
        public IActionResult ViewProduct(int id)
        {
            Product product = _context.Product.Include(seller => seller.Seller).Include(productStatus => productStatus.ProductStatus).Include(subCategory => subCategory.SubCategory).Where(product => product.Id == id).FirstOrDefault();
            return View(product);
        }

        public IActionResult DeleteProduct(int id)
        {
            Product product = _context.Product.Where(product => product.Id == id).FirstOrDefault();
            _context.Product.Remove(product);
            _context.SaveChanges();
            return Redirect("/Admin/Product");
        }

        [HttpGet]
        public IActionResult AddOrUpdateProduct(int id)
        {
            ViewBag.ProductStatus = _context.ProductStatus.ToList();
            ViewBag.SubCategory = _context.SubCategory.ToList();
            ViewBag.Seller = _context.User.ToList();

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

        [HttpPost]
        public IActionResult AddOrUpdateProduct(Product product)
        {
            _context.Product.Update(product);
            _context.SaveChanges();
            return Redirect("/Admin/Product");
        }
        public IActionResult ProductStatus()
        {
            List<ProductStatus> productStatus = _context.ProductStatus.ToList();
            return View(productStatus);
        }

        public IActionResult ViewProductStatus(int id)
        {
            ProductStatus productStatus = _context?.ProductStatus.Where(x => x.Id == id).FirstOrDefault();
            return View(productStatus);
        }
        public IActionResult DeleteProductStatus(int id)
        {
            ProductStatus productStatus = _context?.ProductStatus?.Where(x => x.Id == id).FirstOrDefault();
            _context.ProductStatus.Remove(productStatus);
            _context.SaveChanges();
            return Redirect("/Admin/ProductStatus");
        }
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
        public IActionResult ViewRole(int id)
        {
            Role role = _context?.Role.Where(x => x.Id == id).FirstOrDefault();
            return View(role);
        }
        public IActionResult DeleteRole(int id)
        {
            Role role = _context?.Role?.Where(x => x.Id == id).FirstOrDefault();
            _context.Role.Remove(role);
            _context.SaveChanges();
            return Redirect("/Admin/Role");
        }
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

        [HttpPost]
        public IActionResult AddOrUpdateRole(Role role)
        {
            _context.Role.Update(role);
            _context.SaveChanges();
            return Redirect("/Admin/Role");
        }

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

        public IActionResult DeleteUser(int id)
        {
            AddressDetail AddressDetail = _context.AddressDetail?.Where(x => x.UserId == id).FirstOrDefault();
            if (AddressDetail is not null)
            {
                _context.AddressDetail?.Remove(AddressDetail);
                _context.SaveChanges();
            }

            Product product = _context.Product?.Where(x => x.SellerId == id).FirstOrDefault();
            if (product is not null)
            {
                _context.Product?.Remove(product);
                _context.SaveChanges();
            }

            List<Models.Images> Images = _context.Images?.Where(x => x.ProductId == id).ToList();
            if (Images is not null)
            {
                foreach (var item in Images)
                {
                    _context.Images?.Remove(item);
                    _context.SaveChanges();
                }

            }

            User user = _context.User.Where(user => user.Id == id).FirstOrDefault();
            _context.User.Remove(user);
            _context.SaveChanges();
            return Redirect("/Admin/User");
        }

        [HttpPost]
        public IActionResult AddOrUpdateUser(User user)
        {
            _context.User.Update(user);
            _context.SaveChanges();
            return Redirect("/Admin/User");
        }

        [HttpGet]
        public IActionResult AddOrUpdateUser(int id = 0)
        {

            ViewBag.Role = _context.Role.ToList();

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