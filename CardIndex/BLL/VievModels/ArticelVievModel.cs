using System;

namespace BLL.VievModels
{
    public class ArticelVievModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string AuthorFullName { get; set; }
        public string ThemeName { get; set; }
        public DateTime Created { get; set; }
        public double AvgRate { get; set; }
    }
}
