using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.Entities;
using MPMSRS.Services.Interfaces;

namespace MPMSRS.Services.Repositories
{
    public class CorrectiveActionAttachmentRepository : UserBase<CorrectiveActionAttachments>, ICorrectiveActionAttachmentProfileRepository
    {
        public CorrectiveActionAttachmentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateCorrectiveActionAttachment(CorrectiveActionAttachments correctiveActionAttachment)
        {
            Create(correctiveActionAttachment);
        }

        public void DeleteCorrectiveActionAttachmentByCorrectiveActionId(List<CorrectiveActionAttachments> listCorrectiveActionAttachment)
        {
            RepositoryContext.CorrectiveActionAttachments.RemoveRange(listCorrectiveActionAttachment);
        }

        public async Task<IEnumerable<CorrectiveActionAttachments>> GetCorrectiveActionAttachmentByCorrectiveActionIdWithoutPage(Guid correctiveActionId)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false && ow.CorrectiveActionId == correctiveActionId)
                .OrderByDescending(ow => ow.CreatedAt)
                .ToListAsync();
        }
    }
}
