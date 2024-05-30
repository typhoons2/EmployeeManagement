namespace QL_NhanVien.DataAccess.DTOs
{
    public class UserResponseDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public decimal ContractSalary { get; set; }
        public int DaysOff { get; set; } 
        public int RoleId { get; set; }
    }
}
