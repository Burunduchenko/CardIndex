using System;

namespace BLL.VievModels
{
    /// <summary>
    /// A model for transmitting to the user the 
    /// necessary information about Article Entity
    /// </summary>
    public class CardVievModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string AuthorFullName { get; set; }
        public string ThemeName { get; set; }
        public DateTime Created { get; set; }
        public double AvgRate { get; set; }
    }
}
