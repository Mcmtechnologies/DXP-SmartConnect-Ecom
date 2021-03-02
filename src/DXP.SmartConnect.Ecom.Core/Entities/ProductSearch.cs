using System.Collections.Generic;

namespace DXP.SmartConnect.Ecom.Core.Entities
{
    public class ProductSearch : ProductBaseSearch
    {
        public IDictionary<string, string> FacetHeaders { set; get; }
        public IDictionary<string, IList<ProductFacet>> Facets { set; get; }
        public string CategoryName { set; get; }
    }

    public class ProductMultiSearch
    {
        public IList<ProductSearch> Items { set; get; }
    }
}
