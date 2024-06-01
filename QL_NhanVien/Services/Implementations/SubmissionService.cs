using AutoMapper;
using QL_NhanVien.DataAccess.DTOs;
using QL_NhanVien.DataAccess.Entities;
using QL_NhanVien.DataAccess.Repositories.Inteface;
using QL_NhanVien.DataAccess.UnitOfWork;
using QL_NhanVien.Services.Interfaces;

namespace QL_NhanVien.Services.Implementations
{
    public class SubmissionService : ISubmissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mappers;
        private readonly IAttachedFileRepository _attachedFileRepository;
        public SubmissionService(IUnitOfWork unitOfWork, IMapper mapper, IAttachedFileRepository attachedFileRepository)
        {
            _unitOfWork = unitOfWork;
            _mappers = mapper;
            _attachedFileRepository = attachedFileRepository;
        }

        public Submission CreateSubmission(SubmissionDTO dto, IFormFile data, int userId)
        {
            try
            {
                var submission = _mappers.Map<Submission>(dto);
                submission.UserId = userId;
                submission.SendDate = DateTime.Now;
                submission.Status = false;
                _unitOfWork.SubmissionObj.CreateSubmission(submission);
                _unitOfWork.Save();

                var attachedFile = new AttachedFile()
                {
                    FileName = data.FileName,
                    SubmissionId = submission.SubmissionId
                };
                using (var stream = new MemoryStream())
                {
                    data.CopyTo(stream);
                    attachedFile.FileData = stream.ToArray();
                }
                _attachedFileRepository.CreateAttachedFile(attachedFile);
                _attachedFileRepository.Save();

                return submission;
            }
            catch(Exception)
            {
                throw;
            }
        }

    }
}
