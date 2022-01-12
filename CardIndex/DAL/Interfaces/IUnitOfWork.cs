using DAL.Entities;

namespace DAL.Interfaces
{
    /// <summary>
    /// The interface is designed to describe the class UnitOfWork
    /// </summary>
    public interface IUnitOfWork
    {
        IRepository<Card> CardRepository { get; }
        IRepository<Theme> ThemeRepository { get; }
        IBaseRepository<CardAssessment> CardAssessmentRepository { get; }

        int SaveChanges();
    }
}
