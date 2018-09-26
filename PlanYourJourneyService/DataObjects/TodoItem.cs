using System;
using Microsoft.Azure.Mobile.Server;

namespace PlanYourJourneyService.DataObjects
{
    /*
    public class TodoItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }
    }
    */

    public class Arrangement : EntityData
    {
        //public long? Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string ImageResourcePath { get; set; }
        public string Contents { get; set; }
        public int Favorite { get; set; }
        public string Author { get; set; }
    }
}