using NLog;

static class Program
{
    private static string movieFile = "data/movies.csv";
    public static NLog.Logger logger = LogManager.LoadConfiguration(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
    private static List<Movie> movies;
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome!");
        movies = retrieveMovies();
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
                readMovies();
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

    private static List<Movie> retrieveMovies()
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
        sr.Close();
        return movies;
    }

    private static List<Movie> readMovies()
    {
        movies.ForEach(movie => Console.WriteLine(movie.ToString()));
        return movies;
    }

    private static void addMovies()
    {
        StreamWriter sw = new StreamWriter(movieFile, true);
        do
        {
            Movie movie = new Movie();
            UInt64 id = 0;
            while (id == 0)
            {
                Console.WriteLine("What is the movie ID?");
                string response = Console.ReadLine();
                UInt64.TryParse(response, out id);
                if (id == 0)
                {
                    Console.WriteLine("Invalid movie ID! Must be greater than 0.");
                }
                else if (movies.Where(m => m.movieId == id).FirstOrDefault() != null)
                {
                    Console.WriteLine("That movie ID already exists! Please use another one.");
                    id = 0;
                }
            }
            movie.movieId = id;

            Console.WriteLine("What is the movie name?");
            string name = Console.ReadLine();
            movie.title = name;

            Console.WriteLine("Please enter the movie genres, separated by |");
            string genres = Console.ReadLine();
            string[] genreList = genres.Split('|');
            movie.genres = new List<string>();
            movie.genres.AddRange(genreList);

            movies.Add(movie);
            sw.WriteLine(movie.Serialize());
        } while (promptForAnother());
        sw.Close();
    }

    private static bool promptForAnother()
    {
        Console.Write("Add another movie? (Y/N) ");
        string? response = Console.ReadLine().ToUpper();
        return response == "Y";
    }
}