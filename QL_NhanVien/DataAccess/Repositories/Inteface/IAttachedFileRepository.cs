using QL_NhanVien.DataAccess.Entities;

namespace QL_NhanVien.DataAccess.Repositories.Inteface
{
    public interface IAttachedFileRepository
    {
        bool CreateAttachedFile(AttachedFile attachedFile);

        AttachedFile GetAttachedFile(int id); // Use AttachedFile instead of Submission

        bool Save();
    }
}
