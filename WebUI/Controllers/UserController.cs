﻿using AutoMapper;
using BLL.DTO;
using BLL.Entity;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        IUserService _user;
        IMapper _mapper;
        IShoppingCart icart;
        ShoppingCartView _cartView;

        public UserController(IUserService user, IMapper mapper, IShoppingCart _icart)
        {
            _user = user;
            _mapper = mapper;
            icart = _icart;
            _cartView = new ShoppingCartView();
        }
        public UserController() { }

        [HttpPost]
        [Route("api/CartPanel/addItem")]
        public void AddItem([FromBody]ItemView item, int quantity)
        {
            var _item = _mapper.Map<ItemDTO>(item);
            
            _user.AddItem(_item, quantity, icart);
        }

        [HttpDelete]
        [Route("api/CartPanel/removeItem")]
        public void RemoveItem([FromBody]ItemView item)
        {
            var _item = _mapper.Map<ItemDTO>(item);
            _user.RemoveItem(_item, icart);
        }

        [HttpPut]
        [Route("api/CartPanel/composeOrder")]
        public IShoppingCart ComposeCart()
        {
            var cart = _user.ComposeCart(icart);

            return cart;
        }

        [HttpPost]
        [Route("api/OrderPanel/addOrder")]
        public OrderView MakeOrder()
        {
            var _order =_user.MakeOrder(icart);
            OrderView _ordermap = _mapper.Map<OrderView>(_order);
            return _ordermap;
        }
    }
}
