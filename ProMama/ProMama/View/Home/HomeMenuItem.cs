using System;

namespace ProMama.View.Home
{
    public class HomeMenuItem
    {
        public HomeMenuItem()
        {
            TargetType = typeof(HomeDetail);
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}