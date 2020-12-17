using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuditSeverity.Models;
using AuditSeverity.Repository;
using Newtonsoft.Json;
namespace AuditSeverity.Provider
{
    public class AuditSeverityProvider: IAuditSeverityProvider
    {
        private IAuditSeverityRepository _repository;
        public AuditSeverityProvider(IAuditSeverityRepository repository)
        {
            _repository = repository;
        }

        public async Task<AuditResponse> GetAuditType(AuditRequest request)
        {
            var outdb = await _repository.GetBenchMarkValues(request);
            return outdb;
        }
    }
}
