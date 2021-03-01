using System;
using System.Collections.Generic;
using System.Text;
namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class DeptSummaryDto
    {
        public string DepartmentId { get; set; }
        public string Department { get; set; }
        public IList<SubDept1sDto> SubDept1s { get; set; }
    }
    public class SubDept1sDto
    {
        public string SubDept1Id { get; set; }
        public string SubDept1Parent { get; set; }
        public string SubDepartment1 { get; set; }
        public IList<SubDept2sDto> SubDept2s { get; set; }
    }

    public class SubDept2sDto
    {
        public string SubDept2Id { get; set; }
        public string SubDept2Parent { get; set; }
        public string SubDepartment2 { get; set; }
    }
}
