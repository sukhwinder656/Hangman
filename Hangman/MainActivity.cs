using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;
using Android.Views.InputMethods;
using Android.Content;
using System.Linq;

namespace Hangman
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme",  Icon = "@mipmap/ic_launcher")]
    public class MainActivity : AppCompatActivity
    {
        //Declare objects 
        public List<Player> pList;
        public List<Player> updateData;
        public TextView tName;
        public ListView playerList;
        public CheckBox chkEasy, chkNormal, chkDifficult;
        public DatabaseManager dbManager;
        public Button btnplay, btnadd;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            btnplay = FindViewById<Button>(Resource.Id.btnPlay);
            btnplay.Click += Play_Game;
            btnadd = FindViewById<Button>(Resource.Id.btnAdd);
            btnadd.Click += Btnadd_Click;
            tName = FindViewById<TextView>(Resource.Id.txtName);
            chkEasy = FindViewById<CheckBox>(Resource.Id.cbxEasy);
            chkNormal = FindViewById<CheckBox>(Resource.Id.cbxNormal);
            chkDifficult = FindViewById<CheckBox>(Resource.Id.cbxDifficult);
            playerList = FindViewById<ListView>(Resource.Id.listView1);
            playerList.ItemClick += PlayerList_ItemClick1;
            dbManager = new DatabaseManager();
            UpdatePlayerList();
            updateData = pList;
            PlayerAdapter myAdapter = new PlayerAdapter(this, pList);
            playerList.Adapter = myAdapter;
// Check if entered name already exist in database                          
            tName.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {
                // filter Playerlist on text changed
                var searchTerm = tName.Text;
                updateData = pList.Where(player => player.PlayerName.ToLower().Contains(searchTerm.ToLower())
               ).ToList();
                var filteredResultsAdapter = new PlayerAdapter(this, updateData);
                playerList.Adapter = filteredResultsAdapter;
            };
        }
        //Show player name  in textbox based upon selection on list view item
        private void PlayerList_ItemClick1(object sender, AdapterView.ItemClickEventArgs e)
        {
            var listView = sender as ListView;
             CheckGame.CurrentPlayer = updateData[e.Position];
            tName.Text = CheckGame.CurrentPlayer.PlayerName;
            HideKeyboard();
        }
        protected override void OnResume()
        {
            base.OnResume();
            UpdatePlayerList();
            tName.Text = "";

        }

        public void HideKeyboard()
        {
            InputMethodManager inputManager = (InputMethodManager)GetSystemService(Context.InputMethodService);
            var currentFocus = CurrentFocus;

            inputManager.HideSoftInputFromWindow(currentFocus.WindowToken, HideSoftInputFlags.None);
        }
        //Sort list name wise and update list view
        void UpdatePlayerList()
        {
            pList = dbManager.ViewAllSortByName();
            PlayerAdapter myDataAdapter = new PlayerAdapter(this, pList);
            playerList.Adapter = myDataAdapter;
        }

       
        //Add player name in database.
        private void Btnadd_Click(object sender, System.EventArgs e)
        {
            if(tName.Text!=null)
            {
                string pn = tName.Text;
                dbManager.add(pn);
                UpdatePlayerList();
                tName.Text = "";

            }
            else
            {
                Toast.MakeText(this, "Enter or Select a Player", ToastLength.Long).Show();
            }
        }
        // Validating entered data and set difficulty level according to player choice
        private void Play_Game(object sender, System.EventArgs e)
        {
            if (chkEasy.Checked == false && chkNormal.Checked == false && chkDifficult.Checked == false)
            {
                Toast.MakeText(this, "Select Difficulty", ToastLength.Long).Show();
            }
            else if (CheckGame.CurrentPlayer == null || CheckGame.CurrentPlayer.PlayerName != tName.Text)
            {
                Toast.MakeText(this, "Select Player", ToastLength.Long).Show();
            }
            else
            {
                CheckGame.Easy = chkEasy.Checked;
                CheckGame.Normal = chkNormal.Checked;
                CheckGame.Difficult = chkDifficult.Checked;
                StartActivity(typeof(Play));
            }

        }
    }
}