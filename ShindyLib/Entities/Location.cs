﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace EventLibrary.Entities
{

    public class Location
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LocationURI { get; set; }

        public Event HostedEvents { get; set; }

        public virtual Event Event { get; set; }

        public virtual Address Address { get; set; }

    }
}

