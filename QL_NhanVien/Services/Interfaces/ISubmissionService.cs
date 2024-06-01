using QL_NhanVien.DataAccess.DTOs;
using QL_NhanVien.DataAccess.Entities;

namespace QL_NhanVien.Services.Interfaces
{
    public interface ISubmissionService
    {
        Submission CreateSubmission(SubmissionDTO dto, IFormFile data, int userId);
    }
}
