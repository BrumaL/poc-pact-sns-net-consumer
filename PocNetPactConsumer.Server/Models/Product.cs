using System;
using System.Collections.Generic;
using System.Text;

namespace PocNetPactConsumer.Server.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }

        protected bool Equals(Product other)
        {
            return Id == other.Id && string.Equals(Name, other.Name) && string.Equals(Color, other.Color) && Price == other.Price;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Product)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Color, Price);
        }
    }
}
