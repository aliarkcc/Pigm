using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class FollowList:IEntity
    {
        public int Id { get; set; }
        public string InvoiceNo { get; set; }
        public string Segment { get; set; }
        public string SerialNo { get; set; }
        public string Well { get; set; }
        public string PigmNo { get; set; }
        public string PetitionNo { get; set; }
        public string Used{ get; set; }
        public string FrontIndexStatus{ get; set; }
        public string Explanation { get; set; }
        public DateTime?  RecourseDate{ get; set; }
        public string TpsNo { get; set; }
        public string DocumentNo { get; set; }
        public DateTime? RatificationDate{ get; set; }
        public int CompanyId { get; set; }
        public int ImportExportId { get; set; }
    }
}
