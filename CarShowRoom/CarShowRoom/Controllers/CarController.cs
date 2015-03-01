using CarShowRoom.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarShowRoom.Controllers
{
    public class CarController : ApiController
    {
        //Stub for testing without database
        //CarMake[] carMakes = new CarMake[] 
        //{ 
        //    new CarMake { ID = 1, Make = "Ford" }, 
        //    new CarMake { ID = 2, Make = "BMW" }, 
        //    new CarMake { ID = 3, Make = "TATA" },
        //    new CarMake { ID = 4, Make = "Hundai" },
        //    new CarMake { ID = 5, Make = "Holden"}
        //};

        //CarModel[] carModels = new CarModel[] 
        //{ 
        //    new CarModel { ID = 1, CarModel  = "24f" }, 
        //    new CarModel { ID = 2, CarModel = "434" }, 
        //    new CarModel { ID = 3, CarModel = "fd" }, 
        //    new CarModel { ID = 2, CarModel = "fd" }, 
        //    new CarModel { ID = 3, CarModel = "fdf" }
        //};

        CarDBEntities db = new CarDBEntities();
        // GET api/car
        public object Get()
        {
            var carMakes = db.CarMakes.AsEnumerable();
            var carModels = db.CarModels.AsEnumerable();
            object objForm = new { carMakes, carModels };
            return objForm;
        }

        // GET api/car/5
        public IEnumerable<CarModel> Get(int id)
        {
            var carModelsByMakeId = db.CarModels.Where(t => t.Make_ID == id);
            if (carModelsByMakeId == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return carModelsByMakeId;
        }
        [HttpGet]
        // [Route("Car/Makes")]
        public IEnumerable<CarMake> GetAll()
        {
            return db.CarMakes.AsEnumerable();
        }
        // POST api/CarModel
        [HttpPost]
        public HttpResponseMessage Post(CarModel newmodel)
        {
            if (ModelState.IsValid)
            {
                db.CarModels.Add(newmodel);
                db.SaveChanges();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, newmodel);
                response.Headers.Location = new Uri(Url.Link("DefaultApiWithId", new { id = newmodel.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

    }
}
