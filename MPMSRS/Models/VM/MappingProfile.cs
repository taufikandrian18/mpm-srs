using System;
using AutoMapper;
using MPMSRS.Models.Entities;

namespace MPMSRS.Models.VM
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Users, UserDto>();
            CreateMap<UserForCreationDto, UserDto>();
            CreateMap<UserForCreationDto, Users>();
            CreateMap<UserForUpdateDto, Users>();
            CreateMap<UserForDeleteDto, Users>();

            CreateMap<Networks, NetworkDto>();
            CreateMap<NetworkForCreationDto, Networks>();
            CreateMap<NetworkForUpdateDto, Networks>();
            CreateMap<NetworkForDeleteDto, Networks>();

            CreateMap<Positions, PositionDto>();
            CreateMap<PositionCreationForDto, Positions>();
            CreateMap<PositionUpdateForDto, Positions>();
            CreateMap<PositionDeleteForDto, Positions>();

            CreateMap<Divisions, DivisionDto>();
            CreateMap<DivisionCreationForDto, Divisions>();
            CreateMap<DivisionUpdateForDto, Divisions>();
            CreateMap<DivisionDeleteForDto, Divisions>();

            CreateMap<Roles, RoleDto>();
            CreateMap<RoleForCreationDto, Roles>();
            CreateMap<RoleForUpdateDto, Roles>();
            CreateMap<RoleForDeleteDto, Roles>();

            CreateMap<ProblemCategories, ProblemCategoryDto>();
            CreateMap<ProblemCategoryCreationForDto, ProblemCategories>();
            CreateMap<ProblemCategoryUpdateForDto, ProblemCategories>();
            CreateMap<ProblemCategoryDeleteForDto, ProblemCategories>();

            CreateMap<VisitingTypes, VisitingTypeDto>();
            CreateMap<VisitingTypeForCreationDto, VisitingTypes>();
            CreateMap<VisitingTypeForUpdateDto, VisitingTypes>();
            CreateMap<VisitingTypeForDeleteDto, VisitingTypes>();

            CreateMap<Visitings, VisitingDto>();
            CreateMap<VisitingForCreationDto, Visitings>();
            CreateMap<VisitingForUpdateDto, Visitings>();
            CreateMap<VisitingForDeleteDto, Visitings>();

            CreateMap<VisitingPeoples, VisitingPeopleDto>();
            CreateMap<VisitingPeopleCreationDto, VisitingPeoples>();
            CreateMap<VisitingPeopleUpdateDto, VisitingPeoples>();
            CreateMap<VisitingPeopleDeleteDto, VisitingPeoples>();

            CreateMap<VisitingNoteMappings, VisitingNoteMappingDto>();
            CreateMap<VisitingNoteMappingForCreationDto, VisitingNoteMappings>();
            CreateMap<VisitingNoteMappingForUpdateDto, VisitingNoteMappings>();
            CreateMap<VisitingNoteMappingForDeleteDto, VisitingNoteMappings>();

            CreateMap<UserNetworkMappings, UserNetworkMappingDto>();
            CreateMap<UserNetworkMappingForCreationDto, UserNetworkMappings>();
            CreateMap<UserNetworkMappingForUpdateDto, UserNetworkMappings>();
            CreateMap<UserNetworkMappingForDeleteDto, UserNetworkMappings>();

            CreateMap<Authentications, AuthenticationDto>();
            CreateMap<AuthenticationForCreationDto, Authentications>();
            CreateMap<AuthenticationForUpdateDto, Authentications>();
            CreateMap<AuthenticationForDeleteDto, Authentications>();

            CreateMap<VisitingDetailReports, VisitingDetailReportDtoModel>();
            CreateMap<VisitingDetailReportForCreationDto, VisitingDetailReports>();
            CreateMap<VisitingDetailReportDeadlineUpdateDto, VisitingDetailReports>();
            CreateMap<VisitingDetailReportForUpdateDto, VisitingDetailReports>();
            CreateMap<VisitingDetailReportForDeleteDto, VisitingDetailReports>();

            CreateMap<VisitingDetailReportProblemCategories, VisitingDetailReportProblemCategoryDto>();
            CreateMap<VisitingDetailReportProblemCategoryForCreationDto, VisitingDetailReportProblemCategories>();

            CreateMap<VisitingDetailReportPICs, VisitingDetailReportPicDto>();
            CreateMap<VisitingDetailReportPicForCreationDto, VisitingDetailReportPICs>();

            CreateMap<VisitingDetailReportAttachments, VisitingDetailReportAttachmentDto>();
            CreateMap<VisitingDetailReportAttachmentForCreationDto, VisitingDetailReportAttachments>();

            CreateMap<CorrectiveActions, CorrectiveActionDto>();
            CreateMap<CorrectiveActionForCreationDto, CorrectiveActions>();
            CreateMap<CorrectiveActionForUpdateDto, CorrectiveActions>();

            CreateMap<CorrectiveActionAttachments, CorrectiveActionAttachmentDto>();
            CreateMap<CorrectivActionAttachmentForCreationDto, CorrectiveActionAttachments>();

            CreateMap<UserProblemCategoryMappings, UserProblemCategoryMappingDto>();
            CreateMap<UserProblemCategoryMappingForCreationDto, UserProblemCategoryMappings>();
            CreateMap<UserProblemCategoryMappingForUpdateDto, UserProblemCategoryMappings>();
            CreateMap<UserProblemCategoryMappingForDeleteDto, UserProblemCategoryMappings>();

            CreateMap<UserFcmTokens, UserFcmTokenDto>();
            CreateMap<UserFcmTokenForCreationDto, UserFcmTokenDto>();
            CreateMap<UserFcmTokenForCreationDto, UserFcmTokens>();
            CreateMap<UserFcmTokenForUpdateDto, UserFcmTokens>();
            CreateMap<UserFcmTokenForDeletionDto, UserFcmTokens>();

            CreateMap<EventMasterDatas, EventMasterDataDto>();
            CreateMap<EventMasterDataCreationDto, EventMasterDatas>();
            CreateMap<EventMasterDataUpdateDto, EventMasterDatas>();
            CreateMap<EventMasterDataDeletionDto, EventMasterDatas>();

            CreateMap<EventPeoples, EventPeopleDto>();
            CreateMap<EventPeopleForCreationDto, EventPeoples>();
            CreateMap<EventPeopleForUpdateDto, EventPeoples>();
            CreateMap<EventPeopleForDeletionDto, EventPeoples>();

            CreateMap<Events, EventDto>();
            CreateMap<EventForCreationDto, Events>();
            CreateMap<EventForUpdateDto, Events>();
            CreateMap<EventForDeletionDto, Events>();

            CreateMap<EventDetailReports, EventDetailReportDtoModel>();
            CreateMap<EventDetailReportForCreationDtoModel, EventDetailReports>();
            CreateMap<EventDetailReportForCreationDto, EventDetailReports>();
            CreateMap<EventDetailReportForUpdateDto, EventDetailReports>();
            CreateMap<EventDetailReportForDeletionDto, EventDetailReports>();

            CreateMap<EventDetailReportProblemCategories, EventDetailReportProblemCategoryDto>();
            CreateMap<EventDetailReportProblemCategoryForCreationDto, EventDetailReportProblemCategories>();

            CreateMap<EventDetailReportPICs, EventDetailReportPicDto>();
            CreateMap<EventDetailReportPicForCreationDto, EventDetailReportPICs>();
            CreateMap<EventDetailReportForUpdateDtoModel, EventDetailReports>();
            CreateMap<EventDetailReportPicForCreationViewDto, EventDetailReportPICs>();

            CreateMap<EventDetailReportAttachments, EventDetailReportAttachmentDto>();
            CreateMap<EventDetailReportAttachmentForCreationDto, EventDetailReportAttachments>();
            CreateMap<EventDetailReportAttachmentForCreationViewDto, EventDetailReportAttachments>();

            CreateMap<EventCAs, EventCADto>();
            CreateMap<EventCAForCreationDto, EventCAs>();
            CreateMap<EventCAForUpdateDto, EventCAs>();

            CreateMap<EventCAAttachments, EventCAAttachmentDetail>();
            CreateMap<EventCAAttachmentForCreationViewDto, EventCAAttachments>();

            CreateMap<Checklists, ChecklistDto>();
            CreateMap<ChecklistForCreationDto, Checklists>();
            CreateMap<ChecklistDeadlineUpdateDto, Checklists>();
            CreateMap<ChecklistForUpdateDto, Checklists>();
            CreateMap<ChecklistForDeleteDto, Checklists>();

            CreateMap<ChecklistPICs, ChecklistPicDto>();
            CreateMap<ChecklistPicForCreationDto, ChecklistPICs>();

            CreateMap<ChecklistEvidences, ChecklistEvidenceDto>();
            CreateMap<ChecklistEvidenceForCreationDto, ChecklistEvidences>();

            CreateMap<PushNotifications, PushNotificationDto>();
            CreateMap<PushNotificationForCreationDto, PushNotifications>();
            CreateMap<PushNotificationForUpdateDto, PushNotifications>();
            CreateMap<PushNotificationForDeletionDto, PushNotifications>();

            CreateMap<Ranks, RankDto>();
            CreateMap<RankForCreationDto, Ranks>();
            CreateMap<RankForUpdateDto, Ranks>();
            CreateMap<RankForDeletionDto, Ranks>();

        }
    }
}
