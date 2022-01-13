using System.ComponentModel.DataAnnotations;

namespace BLL.AddModels
{
    /// <summary>
    /// A model for which contains fields and 
    /// validation, for the user to enter data about Theme
    /// </summary>
    public class ThemeAddModel
    {
        public string Name { get; set; }
    }
}
