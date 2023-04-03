using NLog;

static class Program
{
    private static string movieFile = "data/movies.csv";
    public static NLog.Logger logger = LogManager.LoadConfiguration(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
    private static List<Movie> movies;
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome!");
        string response;
        do
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1) Read movies");
            Console.WriteLine("2) Add new movies");
            Console.WriteLine("Press any other key to exit.");
            response = Console.ReadLine();
            if (response == "1")
            {
                movies = readMovies();
            }
            else if (response == "2")
            {
                addMovies();
            }
        }
        while (response == "1" | response == "2");

        StreamWriter sw = new StreamWriter(movieFile, true);
        
        sw.Close();
    }

    private static List<Movie> readMovies()
    {
        List<Movie> movies = new List<Movie>();
        StreamReader sr = new StreamReader(movieFile);
        sr.ReadLine();
        while (!sr.EndOfStream)
        {
            Movie movie = Movie.Deserialize(sr.ReadLine());
            if (movie != null)
            {
                movies.Add(movie);
            }
        }
        movies.ForEach(movie => Console.WriteLine(movie.ToString()));
        sr.Close();
        return movies;
    }

    private static void addMovies()
    {
        
    }
}