using System;
using Microsoft.Extensions.Configuration;
using MPMSRS.Models.Entities;
using MPMSRS.Services.Interfaces;
using MPMSRS.Services.Interfaces.Auth;
using MPMSRS.Services.Interfaces.FCM;
using MPMSRS.Services.Repositories.Auth;
using MPMSRS.Services.Repositories.Command;
using MPMSRS.Services.Repositories.FCM;

namespace MPMSRS.Services.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IUserProfileRepository _user;
        private INetworkProfileRepository _network;
        private IPositionProfileRepository _position;
        private IRoleProfileRepository _role;
        private IDivisionProfileRepository _division;
        private IProblemCategoryProfileRepository _problemCategory;
        private IVisitingTypeProfileRepository _visitingType;
        private IAuthenticationProfileRepository _authentication;
        private IUserNetworkProfileRepository _userNetworkMapping;
        private IVisitingProfileRepository _visiting;
        private IVisitingPeopleProfileRepository _visitingPeople;
        private IVisitingNoteMappingProfileRepository _visitingNoteMapping;
        private IVisitingDetailReportProfileRepository _visitingDetailReport;
        private IVisitingDetailReportPCProfileRepository _visitingDetailReportPc;
        private IVisitingDetailReportPICProfileRepository _visitingDetailReportPic;
        private IVisitingDetailReportAttachmentProfileRepository _visitingDetailReportAttachment;
        private ICorrectiveActionProfileRepository _correctiveAction;
        private ICorrectiveActionAttachmentProfileRepository _correctiveActionAttachment;
        private IUserProblemCategoryProfileRepository _userProblemCategoryMapping;
        private IUserFcmTokenProfileRepository _userFcmToken;
        private IEventMasterDataProfileRepository _eventMasterData;
        private IEventProfileRepository _event;
        private IEventPeopleProfileRepository _eventPeople;
        private IEventDetailReportProfileRepository _eventDetailReport;
        private IEventDetailReportPICProfileRepository _eventDetailReportPic;
        private IEventDetailReportPCProfileRepository _eventDetailReportPc;
        private IEventDetailReportAttachmentProfileRepository _eventDetailReportAttachment;
        private IEventCAProfileRepository _eventCA;
        private IEventCAAttachmentProfileRepository _eventCAAttachment;
        private IPushNotificationProfileRepository _pushNotification;
        private IChecklistProfileRepository _checklist;
        private IChecklistPICProfileRepository _checklistPic;
        private IChecklistEvidenceProfileRepository _checklistEvidence;
        private IRankProfileRepository _rank;

        public IUserProfileRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }
                return _user;
            }
        }

        public INetworkProfileRepository Network
        {
            get
            {
                if (_network == null)
                {
                    _network = new NetworkRepository(_repoContext);
                }
                return _network;
            }
        }

        public IPositionProfileRepository Position
        {
            get
            {
                if (_position == null)
                {
                    _position = new PositionRepository(_repoContext);
                }
                return _position;
            }
        }

        public IRoleProfileRepository Role
        {
            get
            {
                if (_role == null)
                {
                    _role = new RoleRepository(_repoContext);
                }
                return _role;
            }
        }

        public IDivisionProfileRepository Division
        {
            get
            {
                if (_division == null)
                {
                    _division = new DivisionRepository(_repoContext);
                }
                return _division;
            }
        }

        public IProblemCategoryProfileRepository ProblemCategory
        {
            get
            {
                if (_problemCategory == null)
                {
                    _problemCategory = new ProblemCategoryRepository(_repoContext);
                }
                return _problemCategory;
            }
        }

        public IVisitingTypeProfileRepository VisitingType
        {
            get
            {
                if (_visitingType == null)
                {
                    _visitingType = new VisitingTypeRepository(_repoContext);
                }
                return _visitingType;
            }
        }

        public IAuthenticationProfileRepository Authentication
        {
            get
            {
                if (_authentication == null)
                {
                    _authentication = new AuthenticationRepository(_repoContext);
                }
                return _authentication;
            }
        }

        public IUserNetworkProfileRepository UserNetworkMapping
        {
            get
            {
                if (_userNetworkMapping == null)
                {
                    _userNetworkMapping = new UserNetworkRepository(_repoContext);
                }
                return _userNetworkMapping;
            }
        }

        public IVisitingProfileRepository Visiting
        {
            get
            {
                if (_visiting == null)
                {
                    _visiting = new VisitingRepository(_repoContext);
                }
                return _visiting;
            }
        }

        public IVisitingPeopleProfileRepository VisitingPeople
        {
            get
            {
                if (_visitingPeople == null)
                {
                    _visitingPeople = new VisitingPeopleRepository(_repoContext);
                }
                return _visitingPeople;
            }
        }

        public IVisitingNoteMappingProfileRepository VisitingNoteMapping
        {
            get
            {
                if (_visitingNoteMapping == null)
                {
                    _visitingNoteMapping = new VisitingNoteMappingRepository(_repoContext);
                }
                return _visitingNoteMapping;
            }
        }

        public IVisitingDetailReportProfileRepository VisitingDetailReport
        {
            get
            {
                if (_visitingDetailReport == null)
                {
                    _visitingDetailReport = new VisitingDetailReportRepository(_repoContext);
                }
                return _visitingDetailReport;
            }
        }

        public IVisitingDetailReportPCProfileRepository VisitingDetailReportProblemCategory
        {
            get
            {
                if (_visitingDetailReportPc == null)
                {
                    _visitingDetailReportPc = new VisitingDetailReportPCRepository(_repoContext);
                }
                return _visitingDetailReportPc;
            }
        }

        public IVisitingDetailReportPICProfileRepository VisitingDetailReportPIC
        {
            get
            {
                if (_visitingDetailReportPic == null)
                {
                    _visitingDetailReportPic = new VisitingDetailReportPICRepository(_repoContext);
                }
                return _visitingDetailReportPic;
            }
        }

        public IVisitingDetailReportAttachmentProfileRepository VisitingDetailReportAttachment
        {
            get
            {
                if (_visitingDetailReportAttachment == null)
                {
                    _visitingDetailReportAttachment = new VisitingDetailReportAttachmentRepository(_repoContext);
                }
                return _visitingDetailReportAttachment;
            }
        }

        public ICorrectiveActionProfileRepository CorrectiveAction
        {
            get
            {
                if (_correctiveAction == null)
                {
                    _correctiveAction = new CorrectiveActionRepository(_repoContext);
                }
                return _correctiveAction;
            }
        }

        public ICorrectiveActionAttachmentProfileRepository CorrectiveActionAttachment
        {
            get
            {
                if (_correctiveActionAttachment == null)
                {
                    _correctiveActionAttachment = new CorrectiveActionAttachmentRepository(_repoContext);
                }
                return _correctiveActionAttachment;
            }
        }

        public IUserProblemCategoryProfileRepository UserProblemCategoryMapping
        {
            get
            {
                if (_userProblemCategoryMapping == null)
                {
                    _userProblemCategoryMapping = new UserProblemCategoryRepository(_repoContext);
                }
                return _userProblemCategoryMapping;
            }
        }

        public IUserFcmTokenProfileRepository UserFcmToken
        {
            get
            {
                if (_userFcmToken == null)
                {
                    _userFcmToken = new UserFcmTokenRepository(_repoContext);
                }
                return _userFcmToken;
            }
        }

        public IEventMasterDataProfileRepository EventMasterData
        {
            get
            {
                if (_eventMasterData == null)
                {
                    _eventMasterData = new EventMasterDataRepository(_repoContext);
                }
                return _eventMasterData;
            }
        }

        public IEventProfileRepository Event
        {
            get
            {
                if (_event == null)
                {
                    _event = new EventRepository(_repoContext);
                }
                return _event;
            }
        }

        public IEventPeopleProfileRepository EventPeople
        {
            get
            {
                if (_eventPeople == null)
                {
                    _eventPeople = new EventPeopleRepository(_repoContext);
                }
                return _eventPeople;
            }
        }

        public IEventDetailReportProfileRepository EventDetailReport
        {
            get
            {
                if (_eventDetailReport == null)
                {
                    _eventDetailReport = new EventDetailReportRepository(_repoContext);
                }
                return _eventDetailReport;
            }
        }

        public IEventDetailReportPICProfileRepository EventDetailReportPIC
        {
            get
            {
                if (_eventDetailReportPic == null)
                {
                    _eventDetailReportPic = new EventDetailReportPICRepository(_repoContext);
                }
                return _eventDetailReportPic;
            }
        }

        public IEventDetailReportPCProfileRepository EventDetailReportProblemCategory
        {
            get
            {
                if (_eventDetailReportPc == null)
                {
                    _eventDetailReportPc = new EventDetailReportPCRepository(_repoContext);
                }
                return _eventDetailReportPc;
            }
        }

        public IEventDetailReportAttachmentProfileRepository EventDetailReportAttachment
        {
            get
            {
                if (_eventDetailReportAttachment == null)
                {
                    _eventDetailReportAttachment = new EventDetailReportAttachmentRepository(_repoContext);
                }
                return _eventDetailReportAttachment;
            }
        }

        public IEventCAProfileRepository EventCA
        {
            get
            {
                if (_eventCA == null)
                {
                    _eventCA = new EventCARepository(_repoContext);
                }
                return _eventCA;
            }
        }

        public IEventCAAttachmentProfileRepository EventCAAttachment
        {
            get
            {
                if (_eventCAAttachment == null)
                {
                    _eventCAAttachment = new EventCAAttachmentRepository(_repoContext);
                }
                return _eventCAAttachment;
            }
        }

        public IPushNotificationProfileRepository PushNotification
        {
            get
            {
                if (_pushNotification == null)
                {
                    _pushNotification = new PushNotificationRepository(_repoContext);
                }
                return _pushNotification;
            }
        }

        public IChecklistProfileRepository Checklist
        {
            get
            {
                if (_checklist == null)
                {
                    _checklist = new ChecklistRepository(_repoContext);
                }
                return _checklist;
            }
        }

        public IChecklistPICProfileRepository ChecklistPIC
        {
            get
            {
                if (_checklistPic == null)
                {
                    _checklistPic = new ChecklistPICRepository(_repoContext);
                }
                return _checklistPic;
            }
        }

        public IChecklistEvidenceProfileRepository ChecklistEvidence
        {
            get
            {
                if (_checklistEvidence == null)
                {
                    _checklistEvidence = new ChecklistEvidenceRepository(_repoContext);
                }
                return _checklistEvidence;
            }
        }

        public IRankProfileRepository Rank
        {
            get
            {
                if (_rank == null)
                {
                    _rank = new RankRepository(_repoContext);
                }
                return _rank;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
