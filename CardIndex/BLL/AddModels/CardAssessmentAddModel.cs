using System.ComponentModel.DataAnnotations;

namespace BLL.AddModels
{
    /// <summary>
    /// A model for which contains fields and 
    /// validation, for the user to enter data about Article Rate
    /// </summary>
    public class CardAssessmentAddModel
    {
        public int Rate { get; set; }
        public string Comment { get; set; }
        public string CardTitle { get; set; }
        public string UserLogin { get; set; }
    }
}
