using System;

//namespace BookFinder;
//{
    public struct Movie
    {
        public string Title { get; set; }
        public int CopyrightYear { get; set; }
        public string Description { get; set; }
        public int Raiting { get; set; }
        public string Review { get; set; }
        public string ItemType { get; set; }

        public Movie(string title = "", int copyrightYear = 0, string description = "", int raiting = 0, string review = "", string itemType = "")
        {
            Title = title;
            CopyrightYear = copyrightYear;
            Description = description;
            Raiting = raiting;
            Review = review;
            ItemType = itemType;
        }
    };

