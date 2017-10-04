using Xamarin.Forms;

namespace ForeignKeyExample
{
    public partial class App : Application
    {

        static LocalDatabase db;

        public static LocalDatabase DB{
            get{
                if(db == null){
                    db = new LocalDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("Sample.db3"));
                }
                return db;
            }
        }


        public App()
        {
            InitializeComponent();

            MainPage = new ForeignKeyExamplePage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
