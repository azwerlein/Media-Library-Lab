public class Movie
{
    public UInt64 movieId { get; set; }
    public string title { get; set; }
    public string director { get; set; }
    public TimeSpan runningTime { get; set; }
    public List<string> genres { get; set; }

    public static Movie Deserialize(string input)
    {
        string[] substrings = input.Split(',');
        string[] genreList = substrings[2].Split('|');
        Movie movie = new Movie();
        try
        {
            movie.movieId = UInt64.Parse(substrings[0]);
            movie.title = substrings[1];
            movie.genres = new List<string>();
            movie.genres.AddRange(genreList);
            movie.director = substrings.Length > 3 ? substrings[3] : "Unassigned";
            movie.runningTime = substrings.Length > 3 ? TimeSpan.Parse(substrings[4]) : new TimeSpan(0);
        }
        catch (Exception e)
        {
            Program.logger.Error(e.Message);
            return null;
        }
        return movie;
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