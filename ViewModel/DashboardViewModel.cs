using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TheLittleBookNest.ViewModel
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        public string? EventTitle { get; set; }
        public string? Author { get; set; }
        public string? EventDate { get; set; }
        public string? EventLocation { get; set; }
        public string? AuthorBiography { get; set; }
        public string? EmployeeOffer { get; set; }
        public string? BooksHeader { get; set; }
        public string? EmployeeOfferHeader { get; set; }
        public string? AuthorImage { get; set; }
        public string? EmployeeOfferImage { get; set; }
        public ObservableCollection<string> BookImages { get; set; }

        public DashboardViewModel()
        {
            EventTitle = "MÅNADENS EVENT";
            Author = "Om att tänja på gränser - med Johannes Pinter";
            EventDate = "Datum: 2025-02-20";
            EventLocation = "Plats: World Trade Center, Göteborg";
            AuthorBiography = @"Föreläsning om att tänja på genregränser. Han debuterade med sin första roman 2014, skräck/fantasyäventyret ""Vackra kyrkor jag besökt"" på Eskapix. Efter det har romanerna kommit med jämna mellanrum: vampyrromanen ”1007” (2015) på Swedish Zombie, deckartrilogin ”De mörkermärkta” (2017-2021) tillsammans med Mattias Leivinger på Piratförlaget, skräckdeckaren ”Karmakoma” (2021) på Vertigo, och skräcknovellsamlingen ”Mardrömmaren” (2023) på Swedish Zombie. 

Johannes skriver även skräck för barn och har gett ut fjorton titlar i kapitelbokserien ”Monster monster” på Egmont (2018-2023), ”Änglamakerskans barn” (2019) på Egmont och ”Lille Gasten och den stora spökdagen” (2016) på Mörkersdottir förlag. Innan det skönlitterära skrivandet var Johannes mångsysslare inom filmbranschen bl.a. som regissör.";

            EmployeeOffer = "";
            BooksHeader = "Några av hans böcker:";
            EmployeeOfferHeader = "";
            AuthorImage = "/Assets/Pinter.png";
            EmployeeOfferImage = "/Assets/DiscountImage.png";

            // Lista med bokbilder
            BookImages = new ObservableCollection<string>
            {
                "/Assets/Book1.png",
                "/Assets/Book2.png",
                "/Assets/Book3.png"
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
