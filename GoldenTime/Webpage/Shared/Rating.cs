namespace Webpage.Shared
{
    public class Rating
    {
        private POCO.User _user;

        public Rating(POCO.User user)
        {
            _user = user;
        }

        public decimal Calculate(POCO.Post post) 
        {
            decimal rating = 1.0m;

            // User criteria


            // Category criteria

            return rating;
        }
    }
}
