using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using FlickrAPI.Core.Interfaces;

namespace FlickrAPI.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlickrController : ControllerBase
    {
        private readonly IUnitOfWork _connector;
        
        public FlickrController(IUnitOfWork connector)
        {
            _connector = connector;
          
        }

        /// <summary>
        /// Retrieves a set of images from Flickr API
        /// </summary>
        /// <param name="searchString">The category to be searched in Flickr</param>
        /// <returns>200</returns>
        [HttpGet("{searchString}")]
        public async Task<IActionResult> GetPicturesAsync(string searchString)
        {
            try
            {
                
                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    var result = await _connector.SearchRootData.GetAsync(searchString, "flickr");
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    return NoContent();
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
