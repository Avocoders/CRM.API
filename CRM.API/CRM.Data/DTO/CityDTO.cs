using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.DTO
{
    public class CityDTO
    {
        public CityDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }


        public int Id { get; set; }
        public string Name { get; set; }        
    }
}
