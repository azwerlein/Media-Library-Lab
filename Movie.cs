public class Movie
{
    public UInt64 movieId { get; set; }
    public string title { get; set; }
    public string director { get; set; }
    public TimeSpan runningTime { get; set; }
    public List<string> genres { get; set; }

    public static Movie Deserialize(string input)
    {
        try
        {
            string[] substrings = input.Split(',');
            string[] genreList = substrings[2].Split('|');
            Movie movie = new Movie();
            // look for quote(") in string
            // this indicates a comma(,) or quote(") in movie title
            int index = input.IndexOf('"');
            string genres = "";
            if (index == -1)
            {
                movie.movieId = UInt64.Parse(substrings[0]);
                movie.title = substrings[1];
                movie.genres = new List<string>();
                movie.genres.AddRange(genreList);
                movie.director = substrings.Length > 3 ? substrings[3] : "Unassigned";
                movie.runningTime = substrings.Length > 4 ? TimeSpan.Parse(substrings[4]) : new TimeSpan(0);
            }
            else
            {
                // quote = comma or quotes in movie title
                // extract the movieId
                movie.movieId = UInt64.Parse(input.Substring(0, index - 1));
                // remove movieId and first comma from string
                input = input.Substring(index);
                // find the last quote
                index = input.LastIndexOf('"');
                // extract title
                movie.title = input.Substring(0, index + 1);
                // remove title and next comma from the string
                input = input.Substring(index + 2);
                // split the remaining string based on commas
                string[] details = input.Split(',');
                // the first item in the array should be genres 
                genres = details[0];
                // if there is another item in the array it should be director
                movie.director = details.Length > 1 ? details[1] : "unassigned";
                // if there is another item in the array it should be run time
                movie.runningTime = details.Length > 2 ? TimeSpan.Parse(details[2]) : new TimeSpan(0);
            }
            return movie;
        }
        catch (Exception e)
        {
            Program.logger.Error(e.Message);
            return null;
        }
    }

    public string Serialize()
    {
        string genreList = string.Join('|', genres);
        return $"{movieId},{title},{genreList}";
    }

    public override string ToString()
    {
        string genreString = string.Join(", ", genres);
        return $"{movieId}, {title}, {genreString}";
    }
}