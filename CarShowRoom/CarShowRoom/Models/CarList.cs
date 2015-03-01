using CarShowRoom.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarShowRoom.Models
{
    public class CarList
    {
        public IList<CarModel> carModel { get; set; }
        public IList<CarMake> carMake { get; set; }
    }
}