using QL_NhanVien.DataAccess.Entities;
using QL_NhanVien.DataAccess.Repositories.Inteface;

namespace QL_NhanVien.DataAccess.Repositories
{
    public class AttachedFileRepository : IAttachedFileRepository
    {
        private readonly QLNhanVienContext _context;
        public AttachedFileRepository(QLNhanVienContext context)
        {
            _context = context;
        }
        public bool CreateAttachedFile(AttachedFile attachedFile)
        {
            _context.AttachedFiles.Add(attachedFile);
            return Save();
        }

        public AttachedFile GetAttachedFile(int id)
        {
            return _context.AttachedFiles.FirstOrDefault(u => u.AttachedFileId == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
