using System.Collections.Generic;
using OrderForm.Domain.Entities;

namespace OrderForm.Domain.Abstract
{
    public interface ICountryRepository
    {
        IEnumerable<Country> Countries { get; }
    }
}
