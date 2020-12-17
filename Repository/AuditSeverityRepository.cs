using AuditSeverity.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AuditSeverity.Repository
{
    public class AuditSeverityRepository : IAuditSeverityRepository
    { 
        Random rand = new Random();
        public async Task<AuditResponse> GetBenchMarkValues(AuditRequest request)
        {
            List<AuditBenchmark> auditList = new List<AuditBenchmark>();
            using (var client = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                using (var response = await client.GetAsync("http://40.88.229.55/api/AuditBenchmark"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    auditList = JsonConvert.DeserializeObject<List<AuditBenchmark>>(apiResponse);
                }
            }
            string typeofAudit = auditList[0].AuditType;
            string typeofAudit1 = auditList[1].AuditType;
            int benchMarkNo = auditList[0].BenchmarkNoAnswers;
            int benchMarkNo1 = auditList[1].BenchmarkNoAnswers;
            AuditResponse auditResponse;

            if (request.AuditDetail.AuditType.Equals(typeofAudit) && benchMarkNo <= request.AuditDetail.CountOfNos)
            {
                auditResponse = new AuditResponse();
                auditResponse.AuditId = rand.Next();
                auditResponse.ProjectExecutionStatus = "GREEN";
                auditResponse.RemedialActionDuration = "No action needed";
                return auditResponse;
            }
            else if (request.AuditDetail.AuditType.Equals(typeofAudit) && benchMarkNo > request.AuditDetail.CountOfNos)
            {
                auditResponse = new AuditResponse();
                auditResponse.AuditId = rand.Next();
                auditResponse.ProjectExecutionStatus = "RED";
                auditResponse.RemedialActionDuration = "Action to be taken in 2 weeks";
                return auditResponse;
            }
            else if (request.AuditDetail.AuditType.Equals(typeofAudit1) && benchMarkNo1 <= request.AuditDetail.CountOfNos)
            {
                auditResponse = new AuditResponse();
                auditResponse.AuditId = rand.Next();
                auditResponse.ProjectExecutionStatus = "GREEN";
                auditResponse.RemedialActionDuration = "No action needed";
                return auditResponse;
            }
            else if (request.AuditDetail.AuditType.Equals(typeofAudit1) && benchMarkNo1 > request.AuditDetail.CountOfNos)
            {
                auditResponse = new AuditResponse();
                auditResponse.AuditId = rand.Next();
                auditResponse.ProjectExecutionStatus = "RED";
                auditResponse.RemedialActionDuration = "Action to be taken in 1 week";
                return auditResponse;
            }
            else
            {
                return null;
            }
        }
    }
}
