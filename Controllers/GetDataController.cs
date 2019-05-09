namespace pathways_common.Controllers
{
    using System.Collections.Generic;
    using Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class GetDataController<T> : ApiController
    {
        private readonly IGetDataService<T> service;

        public GetDataController(IGetDataService<T> service)
        {
            this.service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            IEnumerable<T> locations = this.service.GetAll();
            return this.Ok(locations);
        }
        
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetById(int id)
        {
            T item = this.service.Retrieve(id);
            return this.Ok(item);
        }
    }
}