using System;
using System.Collections.Generic;
using asistPatentCore.Model.Enums;

namespace asistPatentCore.Model
{
    public class BrandApplicationTypes
    {
        public int id { get; set; } 
        public string name { get; set; }
        public string code { get; set; }
        public ApplicationTypesEnum applicationType { get; set; }
        public ApplicationCategoryTypesEnum categoryType { get; set; }
        public virtual ICollection<Basket> brandApplicationBasketList { get; set; }
        public virtual ICollection<BrandApplicationPrices> BrandApplicationPricesRelation { get; set; }
        public virtual ICollection<BrandApplicationVisibilty> brandApplicationVisibilityList { get; set; }
        public DateTime userCreateDate { get; set; }
    }
}
