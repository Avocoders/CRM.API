using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.DTO
{
    public class RoleDTO
    {
        public RoleDTO(byte id, string name)
        {
            Id = id;
            Name = name;
        }


        public byte Id { get; set; }
        public string Name { get; set; }        
    }
}
