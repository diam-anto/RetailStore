using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailStore.Data;
using RetailStore.Models;

namespace RetailStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly RetailStoreContext _context;

        public PurchasesController(RetailStoreContext context)
        {
            _context = context;
        }

        // GET: api/Purchases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Purchase>>> GetPurchases()
        {
          if (_context.Purchase == null)
          {
              return NotFound();
          }
            return await _context.Purchase.ToListAsync();
        }

        // GET: api/Purchases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Purchase>> GetPurchase(int id)
        {
          if (_context.Purchase == null)
          {
              return NotFound();
          }
            var purchase = await _context.Purchase.FindAsync(id);

            if (purchase == null)
            {
                return NotFound();
            }

            return purchase;
        }

        // POST: api/Purchases
        [HttpPost]
        public async Task<ActionResult<Purchase>> CreatePurchase(PurchaseModel model)
        {
            if (model.CustomerId == 0 && model.OrderProducts.Count == 0)
                return Problem("There is no customerId selected");

            var customer = await _context.Customer.FindAsync(model.CustomerId);
            if (customer == null)
            {
                return NotFound($"Customer with ID {model.CustomerId} not found.");
            }

            var purchase = new Purchase
            {
                CustomerId = model.CustomerId
            };

            purchase.CreatedDate = DateTime.UtcNow;
            _context.Purchase.Add(purchase);
            await _context.SaveChangesAsync();

            decimal totalCost = await CreateOrderProducts(model.OrderProducts, purchase.Id);

            // Save changes to update TotalCost property
            if (totalCost > 0)
            {
                purchase.TotalCost = totalCost;
                purchase.UpdatedDate = DateTime.UtcNow;
                _context.Purchase.Update(purchase);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction("GetPurchase", new { id = purchase.Id }, purchase);
        }

        //order products 
        private async Task<decimal> CreateOrderProducts(List<OrderProductModel> products, int purchaseId)
        {
            if (products.Count == 0)
                throw new Exception("There is no products selected");

            // Create a list to hold OrderProducts
            var orderProducts = new List<OrderProduct>();
            decimal totalCost = 0;

            foreach (var item in products)
            {
                // Retrieve the product based on item.ProductId
                var product = await _context.Product.FindAsync(item.ProductId);
                if (product == null)
                {
                    throw new Exception($"Product with ID {item.ProductId} not found.");
                }

                // Create a new OrderProduct instance
                var orderProduct = new OrderProduct
                {
                    PurchaseId = purchaseId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                };

                var productTotal = orderProduct.Quantity * product.Price;
                totalCost += productTotal;
                // Add the OrderProduct to the list
                orderProducts.Add(orderProduct);
                orderProduct.CreatedDate = DateTime.Now;
                _context.OrderProduct.Add(orderProduct);
                await _context.SaveChangesAsync();
            }

            return totalCost;
        }
    }
}
