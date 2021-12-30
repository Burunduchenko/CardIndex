using System;

namespace BLL.VievModels
{
    public class ArticleRateVievModel
    {
        public int Rate { get; set; }
        public string Comment { get; set; }
        public string ArticleName { get; set; }
        public string UserLogin { get; set; }
        public DateTime Created { get; set; }
    }
}
