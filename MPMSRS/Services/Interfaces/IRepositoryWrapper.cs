using System;
using MPMSRS.Services.Interfaces.Auth;
using MPMSRS.Services.Interfaces.FCM;
using MPMSRS.Services.Repositories.Command;

namespace MPMSRS.Services.Interfaces
{
    public interface IRepositoryWrapper
    {
        IUserProfileRepository User { get; }
        INetworkProfileRepository Network { get;}
        IPositionProfileRepository Position { get; }
        IRoleProfileRepository Role { get; }
        IDivisionProfileRepository Division { get; }
        IProblemCategoryProfileRepository ProblemCategory { get; }
        IVisitingTypeProfileRepository VisitingType { get; }
        IAuthenticationProfileRepository Authentication { get; }
        IUserNetworkProfileRepository UserNetworkMapping { get; }
        IVisitingProfileRepository Visiting { get; }
        IVisitingPeopleProfileRepository VisitingPeople { get; }
        IVisitingNoteMappingProfileRepository VisitingNoteMapping { get; }
        IVisitingDetailReportProfileRepository VisitingDetailReport { get; }
        IVisitingDetailReportPCProfileRepository VisitingDetailReportProblemCategory { get; }
        IVisitingDetailReportPICProfileRepository VisitingDetailReportPIC { get; }
        IVisitingDetailReportAttachmentProfileRepository VisitingDetailReportAttachment { get; }
        ICorrectiveActionProfileRepository CorrectiveAction { get; }
        ICorrectiveActionAttachmentProfileRepository CorrectiveActionAttachment { get; }
        IUserProblemCategoryProfileRepository UserProblemCategoryMapping { get; }
        IUserFcmTokenProfileRepository UserFcmToken { get; }
        IEventMasterDataProfileRepository EventMasterData { get; }
        IEventProfileRepository Event { get; }
        IEventPeopleProfileRepository EventPeople { get; }
        IEventDetailReportProfileRepository EventDetailReport { get; }
        IEventDetailReportPICProfileRepository EventDetailReportPIC { get; }
        IEventDetailReportPCProfileRepository EventDetailReportProblemCategory { get; }
        IEventDetailReportAttachmentProfileRepository EventDetailReportAttachment { get; }
        IEventCAProfileRepository EventCA { get; }
        IEventCAAttachmentProfileRepository EventCAAttachment { get;  }
        IPushNotificationProfileRepository PushNotification { get; }
        IChecklistProfileRepository Checklist { get; }
        IChecklistPICProfileRepository ChecklistPIC { get; }
        IChecklistEvidenceProfileRepository ChecklistEvidence { get; }
        IRankProfileRepository Rank { get; }
        void Save();
    }
}
