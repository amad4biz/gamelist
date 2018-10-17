using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenGameList.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenGameList.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        // GET: api/<controller>
      /*[HttpGet("GetLatest/{num}")]
        public JsonResult GetLatest(int num)
        {

            var arr = new List<ItemViewModel>();

            for (int i = 1; i <= num; i++) arr.Add(new ItemViewModel()
            {
                Id = i,
                Title = String.Format("Item {0} Title", i),
                Description = String.Format("Item {0} Description", i)
            });

            var settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };

            return new JsonResult(arr, settings);

        }
        */

        [HttpGet("Getlatest/{n}")]
        public IActionResult GetLatest(int n)
        {
            if (n > MaxNumberOfItems) n = MaxNumberOfItems;

            var items = GetSampleItems().OrderByDescending(i =>
            i.CreatedDate).Take(n);

            return new JsonResult(items, DefaultJsonSettings);

        }


        [HttpGet("GetMostViewed/{n}")]
        public  IActionResult GetMostViewed( int n)
        {
            var items = GetSampleItems().OrderByDescending(i =>

            i.ViewCount).Take(n);

            return new JsonResult(items, DefaultJsonSettings);
        }


        [HttpGet("GetRandomView/{n}")]
        public IActionResult GetRandomView(int n)
        {
            var items = GetSampleItems().OrderBy(i => Guid.NewGuid()).Take(n);

            return new JsonResult(items, DefaultJsonSettings);

        }




        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return new JsonResult(GetSampleItems()
                       .Where(i => i.Id == id)
                       .FirstOrDefault()
                      //DefaultJonSettings
                       );
        }


        [HttpGet("GetLatest")]
        public IActionResult GetLatest()
        {
            return GetLatest(DefaultNumberOfItems);
        }
        //not found controller
        [HttpGet()]
        public IActionResult Get() {

            return NotFound(new { Error="non found" });
        }


        //

        //private members methods

        private List<ItemViewModel>  GetSampleItems(int num = 999 )
        {

            List<ItemViewModel> lst = new List<ItemViewModel>();

            DateTime date = new DateTime(2017, 12, 31).AddDays(-num);

            for (int id=1; id <= num; id++)
            {
                lst.Add(new ItemViewModel()
                {
                    Id = id,
                    Title = String.Format("Item {0} Title", id),
                    Description = String.Format("This is a sample description for item {0}: Lorem ipsum color sit amet.", id),
                    CreatedDate = date.AddDays(id),
                    LastModifiedDate = date.AddDays(id),
                    ViewCount = num - id



                });


            }

            return lst;
        }

        //p

        private JsonSerializerSettings DefaultJsonSettings
        {
            get
            {
                return new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                };
            }
        }


        /// <summary>
        /// Returns the default number of items to retrieve when using the
        ///ameterless overloads of the API methods retrieving item lists.
 /// </summary>
 private int DefaultNumberOfItems
        {
            get
            {
                return 5;
            }
        }
        /// <summary>
        /// Returns the maximum number of items to retrieve when using the
       ///I methods retrieving item lists.
 /// </summary>
 private int MaxNumberOfItems
        {
            get
            {
                return 100;
            }
        }





      
    }
}
