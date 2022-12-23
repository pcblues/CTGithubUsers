namespace CTGithubUsers.Models
{
    public class GithubUser
    {
        public string? Name { get;  }
        public string? Login { get;  }
        public string? Company { get;  }
        public int NumberFollowers { get;  }
        public int NumberRepositories { get;  }
        public double AverageFollowersPerRepository {
            get {
                // followers/repositories
                // check div 0 
                //
                if (NumberRepositories == 0)
                {
                    return 0;
                } else
                { 
                    return NumberFollowers/NumberRepositories; 
                }
            
            }
                
        }




    }

}
