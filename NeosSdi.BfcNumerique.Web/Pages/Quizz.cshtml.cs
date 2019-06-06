using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NeosSdi.BfcNumerique.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Twilio.Rest.Api.V2010.Account;

namespace NeosSdi.BfcNumerique.Web.Pages
{
    public class QuizzModel : PageModel
    {
        private const string _fromNumber = "+32460229315";

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
            MessageResource.Create(
                body: $"Vous avez obtenu {Score}/10 au quizz Neos-SDI !",
                from: _fromNumber,
                to: PhoneNumber);

            return RedirectToPage("/Index");
        }
    }
}