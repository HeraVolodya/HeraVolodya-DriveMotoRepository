using DriveMoto.DataBase;
using DriveMoto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Security;
using AutoMapper;

namespace DriveMoto.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]

    public class CartItemControler : Controller
    {
        private readonly APIDbContext dbCartItems;
        private readonly IMapper _mapper;

        public CartItemControler(APIDbContext dbCartItems, IMapper mapper)
        {
            this.dbCartItems = dbCartItems;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetCartItem() => Ok(await dbCartItems.CartItems.ToListAsync());

        [HttpPost]
        
        public async Task<IActionResult> AddCarItem(
            [FromBody] AddCartItemRequest addCartItemRequest)
        {
            try
            {
                var cartItem = new CartItem()
                {
                    Id = Guid.NewGuid(),
                    CleantId = addCartItemRequest.CleantId,
                    ProductId = addCartItemRequest.ProductId

                };
                await dbCartItems.CartItems.AddAsync(cartItem);
                await dbCartItems.SaveChangesAsync();

                var newCartItem = await dbCartItems.CartItems
                    .Include(t => t.Product)
                    .FirstOrDefaultAsync(t => t.Id == cartItem.Id);

                return Ok(_mapper.Map<CartItemDTO>(newCartItem));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
