using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeosSdi.BfcNumerique.Web.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public List<Choice> Choices { get; set; }
        public int Answer { get; set; }
        public int Score { get; set; }
    }
}
