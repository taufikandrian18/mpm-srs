using System;
using System.Collections.Generic;
using MPMSRS.Models.Entities;

namespace MPMSRS.Models.VM
{
    public class UserNetworkMappingDto
    {
        public Guid UserNetworkMappingId { get; set; }
        public Guid NetworkId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class UserNetworkMappingDtoViewModel
    {
        public Guid UserNetworkMappingId { get; set; }
        public Networks Network { get; set; }
    }


    public class UserNetworkMappingForCreationMapDto
    {
        public string[] NetworkId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class UserNetworkMappingForCreationDto
    {
        public Guid NetworkId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class UserNetworkMappingForUpdateDto
    {
        public Guid UserNetworkMappingId { get; set; }
        public Guid NetworkId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class UserNetworkMappingForDeleteDto
    {
        public bool IsDeleted { get; set; }
    }
}
