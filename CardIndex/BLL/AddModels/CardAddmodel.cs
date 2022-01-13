using DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace BLL.AddModels
{
    /// <summary>
    /// A model for which contains fields and 
    /// validation, for the user to enter data about Article 
    /// </summary>
    public class CardAddmodel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string AuthorFullName { get; set; }
        public string ThemeName { get; set; }

        public Theme theme { get; set; }
    }
}
