using System;

//namespace BookFinder;
//{
    public struct Book
    {
        public int Id;
        public string Title { get; set; }
        public int CopyrightYear { get; set; }
        public string Description { get; set; }
        public int Raiting { get; set; }
        public string Review { get; set; }
        public string ItemType { get; set; }

        public Book()
        {
            Id = 0;
            Title = string.Empty;
            CopyrightYear = 0;
            Description = string.Empty;
            Raiting = 0;
            Review = string.Empty;
            ItemType = string.Empty;
        }
    };



