public class Movie
{
    public UInt64 movieId { get; set; }
    public string title { get; set; }
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
        }
        catch
        {
            Program.logger.Error("Error parsing movieID. Returning null.");
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