// MainActivity.cs
[Activity(Label = "SQL Client", MainLauncher = true)]
public class MainActivity : Activity
{
    protected override async void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.activity_main);

        string connectionString = "Server=192.168.1.5;Database=MyDB;User=sa;Password=123;";

        try
        {
            using (var conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                var cmd = new SqlCommand("SELECT * FROM Products", conn);
                var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var name = reader["Name"].ToString();
                    var price = reader.GetDecimal(2);
                    Log.Debug("APP", $"Product: {name} - {price}");
                }
            }
        }
        catch (Exception ex)
        {
            Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
        }
    }
}
