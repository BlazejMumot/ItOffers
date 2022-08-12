using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using ItOffers.Models.Models;
using System.Text.RegularExpressions;

namespace ItOffers.Features.NoFluffJobsFeatures.Commands
{
    public class ScrappOffersToDBCommand
    {
        private const string baseUrl = "https://nofluffjobs.com/backend";
        private const string scrappUrl = "https://nofluffjobs.com/backend?page=";
        private static readonly Regex sWhitespace = new Regex(@"\s+|[^0-9.-]");

        public IEnumerable<ScrappOfferModel> GetBackendOffers()
        {
            var web = new HtmlWeb();

            var defaultPage = web.Load(baseUrl).QuerySelectorAll("li a.page-link");
            var numberOfPages = int.Parse(defaultPage[defaultPage.Count - 2].InnerText);
            Console.WriteLine("Found " + numberOfPages + " pages");
            for (int i = 1; i <= 1; i++)
            {
                var document = web.Load(scrappUrl + i);

                var offerlist = document.QuerySelectorAll(".posting-list-item");


                foreach (var offer in offerlist)
                {
                    double? minSalary, maxSalary, avgSalary;
                    var offerName = offer.QuerySelectorAll("h3").ElementAtOrDefault(0)?.InnerText;
                    var company = offer.QuerySelectorAll(".posting-list-item .d-block").ElementAtOrDefault(0)?.InnerText;
                    var seniority = checkSeniority(offerName);
                    var salary = offer.QuerySelectorAll("nfj-posting-item-tags span").ElementAtOrDefault(0)?.InnerText.Replace("&nbsp;", "").Replace("\n", "");
                    calculateSalary(salary, out minSalary, out maxSalary, out avgSalary);
                    var currency = salary ?? "";
                    currency = currency.Split(" ").Last();
                    var tech = offer.QuerySelectorAll("nfj-posting-item-tags common-posting-item-tag").ElementAtOrDefault(0)?.InnerText.Trim();
                    var location = offer.QuerySelectorAll(".posting-list-item .posting-info__location").ElementAtOrDefault(0)?.InnerText.Trim() ?? "";
                    location = location.Split(',').First();
                    var url = "https://nofluffjobs.com/job" + offer.GetAttributes("href").Last().Value;
                    yield return new ScrappOfferModel(offerName, company, seniority, minSalary, maxSalary, avgSalary, currency, tech, location, url);
                }
                Console.WriteLine("Page: " + i + "/" + numberOfPages + "\n");
            }

        }
        public string checkSeniority(string offerName)
        {
            offerName = offerName.ToUpper();
            if (offerName.Contains("TRAINEE")) return "Trainee";
            if (offerName.Contains("JUNIOR")) return "Junior";
            if (offerName.Contains("SENIOR")) return "Senior";
            if (offerName.Contains("EXPERT")) return "Expert";
            return "Mid";
        }

        public void calculateSalary(string salaryString, out double? minSalary, out double? maxSalary, out double? avgSalary)
        {

            var salary = sWhitespace.Replace(salaryString ?? "", "").Split("-");
            if (salary.Length > 1)
            {
                minSalary = Double.Parse(salary[0]);
                maxSalary = Double.Parse(salary[1]);
                avgSalary = (minSalary + maxSalary) / 2;
            }
            else
            {
                minSalary = null;
                maxSalary = null;
                avgSalary = string.IsNullOrEmpty(salary[0]) ? null : Double.Parse(salary[0]);
            }
        }
    }
}
