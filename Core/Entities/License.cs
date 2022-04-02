using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class License: BaseEntity
    {
        public string? key { get; set; }
        public string? name { get; set; }
        public string? spdx_id { get; set; }
        public string? url { get; set; }
        public string? node_id { get; set; }
        public List<GitHubRepository> Repositories { get; set; } = new List<GitHubRepository>();
    }
}
