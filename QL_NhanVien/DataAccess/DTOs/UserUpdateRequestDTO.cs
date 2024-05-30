namespace QL_NhanVien.DataAccess.DTOs
{
    public class UserUpdateRequestDTO
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public decimal ContractSalary { get; set; }
        public int RoleId { get; set; }
    }
}
