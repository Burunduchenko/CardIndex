using System;

namespace BLL.VievModels
{
    /// <summary>
    /// A model for transmitting to the user the 
    /// necessary information about Article Rate Entity
    /// </summary>
    public class CardAssementVievModel
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public string CardTitle { get; set; }
        public string UserLogin { get; set; }
        public DateTime Date { get; set; }
    }
}
