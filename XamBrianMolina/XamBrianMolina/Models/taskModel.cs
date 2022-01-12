using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamBrianMolina.Models
{
    public class taskModel
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
