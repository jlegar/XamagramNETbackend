using Microsoft.Azure.Mobile.Server;
using System.ComponentModel.DataAnnotations.Schema;

namespace XamagramNETbackend.DataObjects
{
    public class City : EntityData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}