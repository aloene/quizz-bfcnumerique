using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NeosSdi.BfcNumerique.Web.Models;

namespace NeosSdi.BfcNumerique.Web.Pages
{
    public class QuizzModel : PageModel
    {
        private const int _questionsCount = 5;

        public List<Question> Questions { get; set; }

        [BindProperty]
        public Nullable<int> Score { get; set; }

        [BindProperty]
        public List<Question> Answers { get; set; }

        [BindProperty]
        public string PhoneNumber { get; set; }

        public void OnGet()
        {
            Questions = Referential.GetRandomQuestions(_questionsCount);
            Score = null;
        }

        public IActionResult OnPostValidateAnswers()
        {
            Score = Referential.GetScore(Answers
                    .Where(a => a.Answer != 0)
                    .ToDictionary(a => a.Id, a => a.Answer));

            return Page();
        }

        public IActionResult OnPostSendScore()
        {
            

            return Page();
        }
    }
}