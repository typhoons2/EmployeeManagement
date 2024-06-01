using QL_NhanVien.DataAccess.UnitOfWork;
using QL_NhanVien.Services.Interfaces;

namespace QL_NhanVien.Services.Implementations
{
    public class AttachedFileService : IAttachedFileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttachedFileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DownloadFileById(int id)
        {
            try
            {
                string downloadPath = "FileDownloaded";
                var file = _unitOfWork.AttachedFileObj.GetAttachedFile(id);

                if (file == null)
                {
                    throw new FileNotFoundException("File not found with the specified ID.");
                }

                // Assuming your file data is stored in a byte array
                var memoryStream = new MemoryStream(file.FileData);

                // Ensure the download directory exists
                if (!Directory.Exists(downloadPath))
                {
                    Directory.CreateDirectory(downloadPath);
                }

                // Construct the full file path
                var filePath = Path.Combine(downloadPath, file.FileName);

                // Write the file data to the specified path
                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    memoryStream.CopyTo(fileStream);
                }
            }
            catch (Exception ex)
            {
                throw; // Re-throw for handling at a higher level
            }
        }
    }
}
