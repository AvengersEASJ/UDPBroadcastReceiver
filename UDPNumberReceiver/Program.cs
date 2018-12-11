using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPNumberReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            int id = 0;
            int thirst = 0;
            int hunger = 0;
            int task = 0;
            int fun = 0;
            int dress = 0;
            
            UdpClient udpServer = new UdpClient(5000);

            udpServer.EnableBroadcast = true;

            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 5000);

            try
            {
                while (true)
                {
                    Byte[] receiveBytes = udpServer.Receive(ref RemoteIpEndPoint);
    
                    string receivedData = Encoding.ASCII.GetString(receiveBytes);
                    

                    string newString = receivedData.ToString().Replace(' ', ',');

                    String[] arr = newString.Split(',');

                    char[] MyChar = { '(', ',' };
                    string _id = arr[0].Trim(MyChar);
                    id = Int32.Parse(_id);
                    thirst = Int32.Parse(arr[1]);
                    hunger = Int32.Parse(arr[2]);
                    task = Int32.Parse(arr[3]);
                    fun = Int32.Parse(arr[4]);
                    string _dress = arr[5].Trim(MyChar);
                    dress = Int32.Parse(_dress);


                    Console.WriteLine(id + " " + thirst + " " + hunger + " " + " " + task + " " + fun + " " + dress);

                    const string conn = "Server=tcp:frienddb.database.windows.net,1433;Initial Catalog=FriendDatabase;Persist Security Info=False;User ID=avengers;Password=Mads12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                    string sql = "UPDATE FRIENDS SET thirst = '" + thirst + "',  hunger = '" + hunger +
                                  "', task = '" + task + "', fun = '" + fun + "', dress = '" + dress +
                                  "' WHERE friendsID = 4";
                   
                    using (var databaseConnection = new SqlConnection(conn))
                    {

                        using (var command = new SqlCommand(sql, databaseConnection))
                        {
                            
                            databaseConnection.Open();
                            command.ExecuteNonQuery();
                            databaseConnection.Close();



                        }


                    }


                }
            }
            catch (Exception)
            {               
            }
            
        }

    }
}
