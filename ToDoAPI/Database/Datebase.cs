namespace ToDoAPI.Database
{
    public class Datebase
    {

        private static string datebaseAddress = "";

        public string datebase { get; set; }

        public string host { get; set; }
        public string port { get; set; }
        public string userName { get; set; }
        public string password { get; set; }



        public static string GetDateBaseAddress()
        {
            return datebaseAddress;
        }
        public static void SetDatebaseAddress(Datebase date)
        {
            if(date.password == string.Empty)
                datebaseAddress = string.Format(@"Database={0};
                                                 Host={1};
                                                 Port={2};
                                                 User Id={3};", date.datebase, date.host, date.port, date.userName);
            else
                datebaseAddress = string.Format(@"Database={0};
                                                 Host={1};
                                                 Port={2};
                                                 User Id={3}
                                                 Password={4};", date.datebase, date.host, date.port,date.userName, date.password) ;
        }



    }
}
