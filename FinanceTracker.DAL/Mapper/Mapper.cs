using FinanceTracker.DAL.Model;

namespace FinanceTracker.DAL.Dtos
{
    public static class Mapper
    {
        public static PendingUsersDto pendingUsersDto(this UserModel userModel)
        {
            return new PendingUsersDto
            {
                UserId = userModel.UserId,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                EmailId = userModel.EmailId,
                PhoneNumber = userModel.PhoneNumber
            };
        }
        public static ApprovedUsersDto approvedUsersDto(this UserModel userModel)
        {
            return new ApprovedUsersDto
            {
                UserId = userModel.UserId,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                EmailId = userModel.EmailId,
                PhoneNumber = userModel.PhoneNumber,
                isActive = userModel.IsActive
            };
        }
        public static RejectedUsersDto rejectedUsersDto(this UserModel userModel)
        {
            return new RejectedUsersDto
            {
                UserId = userModel.UserId,
                ModifiedDate = userModel.ModifiedDate,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                EmailId = userModel.EmailId,
                PhoneNumber = userModel.PhoneNumber,
                RejectionReason = userModel.RejectionReason
            };
        }
    }
}