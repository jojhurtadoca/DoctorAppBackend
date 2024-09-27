namespace Data.Interfaces.IRepository
{
    public interface IUnitOfWork: IDisposable
    {
        ISpecialtyRepository SpecialtyRepository { get; }

        IDoctorRepository DoctorRepository { get; }

        Task Save();
    }
}
