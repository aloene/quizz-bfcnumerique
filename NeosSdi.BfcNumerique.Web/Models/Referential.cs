using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeosSdi.BfcNumerique.Web.Models
{
    public static class Referential
    {
        private static string[] _availableQuestionsStrings =
        {
            "A quel écrivain doit-on le personnage de Boule-de-Suif ?|Guy de Maupassant|Emile Zola|Jules Vallès|Albert Camus|1",
            "Avec quel chanteur Carole Fredericks & Michael Jones ont-il formé un trio ?|J.-J. Goldman|Jean-Louis Aubert|Michel Polnareff|Daniel Balavoine|1",
            "Quel conseil régional est présidé par Ségolène Royal depuis 2004 ?|Poitou-Charentes|Pays de Loire|Aquitaine|Bretagne|2",
            "Quel nom rime avec Paul-Loup Sulitzer dans \"Foule sentimentale\" de Souchon ?|Claudia Schiffer|Chrysler|Gérard Miller|Forest Whitaker|2",
            "De quel pays Tirana est-elle la capitale ?|L'Albanie|La Croatie|Le Monténégro|La serbie|1",
            "Dans quelle ville se déroule chaque année le Festival interceltique ?|Lorient|Quimper|Vannes|Saint-Malo|1",
            "Quel est l'impératif du verbe peindre à la 2e personne du pluriel ?|Peignez|Peins|Peindez|Peinturons|1",
            "De quel groupe Jim Morrison était-il le chanteur ?|The Doors|The Velvet Underground|The rolling stones|Les 2Be3|2",
            "En géométrie, combien de côtés possède un losange ?|4|1|5|3|1",
            "Quel nom porte le bateau dont Théodore Géricault peint le radeau ?|La Méduse|Le Rafiot|Le Titanic|Nemo|1",
            "Sous quel titre français connaît-on le film \"Vertigo\" d'Alfred Hitchcock ?|Sueurs froides|Psychose|Fenêtre sur cour|Les oiseaux|2",
            "Dans quelle ville française se trouve la Cité de l'espace ?|Toulouse|Paris|Dijon|Lyon|1",
            "Si ce n'est pas un fruit, qu'est-ce qu'un kiwi ?|Un oiseau|Un ours|Un coquillage|Un poisson|2",
            "Quel nom porte le siège de la police londonienne ?|Scotland Yard|Picadilly Circus|Downing Street|Westminster|1",
            "Qui cause des problèmes à Michel Blanc dans \"Grosse Fatigue\" ?|Son sosie|Son pote|Son ex-femme|Son fils|2",
            "Où le général de Gaulle prononce-t-il son fameux : \"Je vous ai compris\" ?|Alger|Paris|Berlin|Ajaccio|1",
            "Quelle traversée Louis Blériot a-t-il réussie en avion le 25 juillet 1909 ?|La Manche|L'atlantique|La méditérannée|Le lac Kir|2",
            "Quel nom spécifique désigne les deux dernières paires de côtes ?|Côtes flottantes|Les côtelettes|Les ribs|Les cotillons|1",
            "De quel pays Michelle Bachelet est- elle la Présidente depuis mars 2006 ?|Le Chili|La France|La suisse|La Nouvelle-Zélande|2",
            "Avec qui Mariam interprète-t-elle les chansons de \"Dimanche à Bamako\" ?|Amadou|Mimie Mathy|Jain|Miriam Makeba|1",
            "Dans les bras de quel dieu grec se retrouve-t-on en s'endormant ?|Morphée|Hercule|Jupiter|Poséidon|1",
            "Quel est la race de Bill, le chien de Boule ?|Cocker|Caniche|Bulldog|Basset|2",
            "Avec la laine de quel animal fait-on des vêtements en cachemire ?|La chèvre|Le mouton|Le lama|L'alpaca|2",
            "Combien d'étoiles comporte le drapeau américain ?|50|51|52|53|3",
            "Sous quel nom est plus connu l'acide désoxyribonucléique ?|ADN|Aspririne|Vitamine C|Vitamine A|2"
        };

        private static readonly List<Question> _availableQuestions;


        static Referential()
        {
            _availableQuestions = _availableQuestionsStrings
                .Select(qs => qs.Split('|'))
                .Select((qs, i) =>
                new Question
                {
                    Id = i,
                    Label = qs[0],
                    Choices = new List<Choice>
                    {
                        new Choice
                        {
                            Id = 1,
                            Label = qs[1]
                        },
                        new Choice
                        {
                            Id = 2,
                            Label = qs[2]
                        },
                        new Choice
                        {
                            Id = 3,
                            Label = qs[3]
                        },new Choice
                        {
                            Id = 4,
                            Label = qs[4]
                        }
                    },
                    Answer = 1,
                    Score = int.Parse(qs[5])
                })
                .ToList();
        }

        public static List<Question> GetRandomQuestions(int count)
        {
            var random = new Random();
            return _availableQuestions
                .OrderBy(q => random.Next())
                .Take(count)
                .Select(q => new Question
                {
                    Id = q.Id,
                    Label = q.Label,
                    Choices = q.Choices.OrderBy(a => random.Next()).ToList(),
                    Answer = q.Answer,
                    Score = q.Score
                })
                .ToList();
        }

        public static int GetScore(Dictionary<int, int> questionAnswers)
        {
            var questionsIds = questionAnswers.Keys.ToList();

            var answers = _availableQuestions
                .Where(q => questionsIds.Contains(q.Id));

            var totalPoints = answers
                .Select(q => q.Score)
                .Sum();

            var score = answers
                .Select(q => q.Answer == questionAnswers[q.Id] ? q.Score : 0)
                .Sum();

            return score != 0
                ? (int)Math.Round(((double)score * 10) / totalPoints)
                : 0;
        }
    }
}
