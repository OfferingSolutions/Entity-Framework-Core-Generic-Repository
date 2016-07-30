using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfferingSolutions.UoWCore.SampleApp.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Thing> Things { get; set; }
    }
}
