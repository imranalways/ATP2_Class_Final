﻿using Inventory_Rest_API.Models;
using Inventory_Rest_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Inventory_Rest_API.Controllers
{
    public class CategoriesController : ApiController
    {
        private CategoryRepository categoryRepository = new CategoryRepository();
        public IHttpActionResult Get()
        {
            return Ok(categoryRepository.GetAll());
        }

        public IHttpActionResult Get(int id)
        {
            var category = categoryRepository.Get(id);

            if (category == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(category);
        }

        public IHttpActionResult Post(Category category)
        {
            categoryRepository.Insert(category);
            return Created("api/categories/" + category.CategoryId, category);
        }

        public IHttpActionResult Put([FromUri] int id, [FromBody] Category category)
        {
            category.CategoryId = id;
            categoryRepository.Update(category);
            return Ok(category);
        }

        public IHttpActionResult Delete(int id)
        {
            categoryRepository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("api/categories/{id}/products")]
        public IHttpActionResult GetProductsByCategoryID(int id)
        {
            ProductRepository productRepository = new ProductRepository();
            return Ok(productRepository.GetProductsByCategory(id));
        }
    }
}
