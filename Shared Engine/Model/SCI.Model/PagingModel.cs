using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Model
{
    public class GenericPagingModel<TModel>
    {
        public GenericPagingModel()
        {
            GenericList = new List<TModel>();
        }

        public List<TModel> GenericList { get; set; }
        public int Count { get; set; }
    }

    public class GenericSinglePagingModel<TModel>
    {
        public GenericSinglePagingModel()
        {
        }

        public TModel GenericModel { get; set; }
        public int Count { get; set; }
    }
}
